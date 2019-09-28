using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 10.0f;

    private int _gunDamage;

    private void Awake()
    {
        //Asserting for having collider set to "Is Trigger" and RigidBody set to not "Use Gravity", mainly because these settings are required to let this approach work correctly
        Debug.Assert(GetComponent<Collider>().isTrigger == true, "Collider is not set to Trigger! Check the isTrigger toggle in the Inspector");
        Debug.Assert(GetComponent<Rigidbody>().useGravity == false, "RigidBody is using gravity! Check the useGravity toggle in the Inspector");

        Destroy(gameObject, _lifeTime);//Makes sure missed bullets automatically get destroyed after _lifeTime seconds
    }
    public void InitializeBullet(Vector3 pBulletVelocity, int pGunDamage)
    {
        Debug.Assert(pGunDamage > 0, "Negative gunDamage? Bullets shouldn't heal...");

        _gunDamage = pGunDamage;

        Debug.Assert(pBulletVelocity.magnitude > 0.0f, "Bullets should fly forward and not hang still in the air...");
        GetComponent<Rigidbody>().velocity = pBulletVelocity;
    }

    private void OnTriggerEnter(Collider pOther)
    {
        Enemy enemy = pOther.GetComponent<Enemy>();
        if (enemy != null)//In other words: Does the colliding object have a Enemy component?
        {
            enemy.ReceiveDamage(_gunDamage, this.transform);
        }
        if (!pOther.CompareTag("Player"))
        {
            Destroy(gameObject);//Destroy bullet on first detection, otherwise a single bullet may do damage twice (or even more times
        }
    }
}

