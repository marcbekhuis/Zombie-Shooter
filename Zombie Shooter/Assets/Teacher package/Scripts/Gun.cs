using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab = null;
    [SerializeField] private Transform _bulletSpawnLocation = null;

    [SerializeField] private int _gunDamage = 1;
    [SerializeField] private float _bulletSpeed = 20;
    
    [SerializeField, Tooltip("The amount of bullet shots per second")]
    private float _fireRate = 5;//Amount of shots per second

    private float _cooldownRatePerBulletShot;//Time between each possible shot
    private float _newBulletTimeStamp;//Time when a new bullet can be fired again

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
        //Only fire a bullet when we are allowed to fire, keeping the fireRate into account
        if (Time.time > _newBulletTimeStamp)
        {
            //Creating a new bullet at the bulletSpawnLocation with the same rotation as the gun has
            GameObject bulletObject = Instantiate(_bulletPrefab, _bulletSpawnLocation.position, Quaternion.LookRotation(transform.forward));
            
            //Getting the bullet Component from the newly created bulletObject, so we can initialize it with some gun properties (mainly because MonoBehaviours and Unity don't work well with constructors)
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            Debug.Assert(bullet != null, "_bulletPrefab is not a bullet!");
            bullet.InitializeBullet(transform.forward * _bulletSpeed, _gunDamage);

            //Updating the timeStamp so we know at which time we are allowed to fire another bullet again
            _newBulletTimeStamp = Time.time + _cooldownRatePerBulletShot;
        }
    }
}
