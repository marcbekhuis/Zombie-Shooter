using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunV2 : MonoBehaviour
{
    public static int maxClipSize = 30;

    [SerializeField] private GameObject _bulletPrefab = null;
    [SerializeField] private Transform _bulletSpawnLocation = null;
    [Space]
    [SerializeField] private int _gunDamage = 1;
    [SerializeField] private float _bulletSpeed = 20;
    [SerializeField] private int startingClips = 5;
    [SerializeField] private Text ammoText;

    [SerializeField, Tooltip("The amount of bullet shots per second")]
    private float _fireRate = 5;//Amount of shots per second
    private List<Clip> clips = new List<Clip>();

    private float _cooldownRatePerBulletShot;//Time between each possible shot
    private float _newBulletTimeStamp;//Time when a new bullet can be fired again

    public class Clip
    {
        public int bullets;

        public Clip(int Bullets)
        {
            bullets = Bullets;
        }
    }

    private void Start()
    {
        for (int x = 0; x < startingClips; x++)
        {
            AddClip(maxClipSize);
        }
        ammoText.text = "Ammo: " + clips[0].bullets;
    }

    private void Awake()
    {
        //Asserting for a valid bullet prefab reference and bullet spawn location
        Debug.Assert(_bulletPrefab != null, "Assign a bullet prefab please");
        Debug.Assert(_bulletSpawnLocation, "Assign a _bulletSpawnLocation please");

        //Asserting on positive bulletSpeed and gunDamage
        Debug.Assert(_bulletSpeed > 0.0f, "Bullet speed cannot be negative or 0!");
        Debug.Assert(_gunDamage > 0, "gunDamage should not heal the player and should do damage");

        //Converting fireRate to cooldown time between shots (mainly because thinking in fireRate makes a lot more sense, but the code needs the cooldown time)
        _cooldownRatePerBulletShot = 1.0f / _fireRate;
        _newBulletTimeStamp = Time.time;
    }

    public void TryToFire()
    {
        if (clips.Count != 0)
        {
            if (clips[0].bullets != 0)
            {
                //Only fire a bullet when we are allowed to fire, keeping the fireRate into account
                if (Time.time > _newBulletTimeStamp)
                {
                    //Creating a new bullet at the bulletSpawnLocation with the same rotation as the gun has
                    GameObject bulletObject = Instantiate(_bulletPrefab, _bulletSpawnLocation.position, Quaternion.LookRotation(transform.forward));
                    clips[0].bullets--;
                    ammoText.text = "Ammo: " + clips[0].bullets;

                    //Getting the bullet Component from the newly created bulletObject, so we can initialize it with some gun properties (mainly because MonoBehaviours and Unity don't work well with constructors)
                    BulletV2 bullet = bulletObject.GetComponent<BulletV2>();
                    Debug.Assert(bullet != null, "_bulletPrefab is not a bullet!");
                    bullet.InitializeBullet(transform.forward * _bulletSpeed, _gunDamage);

                    //Updating the timeStamp so we know at which time we are allowed to fire another bullet again
                    _newBulletTimeStamp = Time.time + _cooldownRatePerBulletShot;
                }
            }
            else if (Time.time > _newBulletTimeStamp)
            {
                Reload();
            }
        }
        else if (Time.time > _newBulletTimeStamp)
        {
            Reload();
        }
    }

    public void AddClip(int bullets)
    {
        clips.Add(new Clip(bullets));
    }

    public void Reload()
    {
        if (clips.Count != 0)
        {
            if (clips[0].bullets == 0)
            {
                clips.RemoveAt(0);
                ammoText.text = "Ammo: " + clips[0].bullets;
                _newBulletTimeStamp = Time.time + 1;
            }
            else
            {
                clips.Add(clips[0]);
                clips.RemoveAt(0);
                ammoText.text = "Ammo: " + clips[0].bullets;
                _newBulletTimeStamp = Time.time + 1;
            }
        }
        else
        {
            ammoText.text = "Out Of Ammo";
        }
    }
}
