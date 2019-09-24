using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    public Map map;

    [SerializeField] private int _health;

    private void Awake()
    {
        //Asserting for positive health
        Debug.Assert(_health > 0, "Enemies also deserve a bit of health...");
    }

    public void ReceiveDamage(int pDamage)
    {
        //Asserting for positive damage value, bullets shouldn't heal enemies :P
        Debug.Assert(pDamage > 0, "You can't do negative or zero damage...");

        _health -= pDamage;

        if (_health >= 0)
        {
            map.zombies.Remove(this.transform);
            Destroy(gameObject);
        }
    }
}
