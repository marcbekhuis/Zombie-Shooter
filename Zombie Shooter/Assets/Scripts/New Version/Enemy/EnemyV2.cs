using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyV2 : MonoBehaviour
{
    public PlayerHealthV2 playerHealth;
    public ScoreSystem scoreSystem;

    [Space]

    [SerializeField]
    private int _health;

    [SerializeField]
    private int damage = 1;

    [Space]

    [SerializeField]
    private int scorePoints;

    [SerializeField]
    private GameObject bulletBloodParticle;

    [SerializeField]
    private GameObject dieBloodParticle;

    private void Awake()
    {
        //Asserting for positive health
        Debug.Assert(_health > 0, "Enemies also deserve a bit of health...");
    }

    public void ReceiveDamage(int pDamage, Transform bulletLocation)
    {
        //Asserting for positive damage value, bullets shouldn't heal enemies :P
        Debug.Assert(pDamage > 0, "You can't do negative or zero damage...");

        _health -= pDamage;
        Instantiate(bulletBloodParticle, bulletLocation.position, bulletLocation.rotation);

        if (_health <= 0)
        {
            Instantiate(dieBloodParticle, this.transform.position, this.transform.rotation);
            scoreSystem.Addscore(scorePoints);
            Destroy(gameObject);
        }
    }

    // Kills the enemy when touching the player, also removes 1 live from the player.
    private void OnCollisionEnter(Collision collision)
    {
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerHealth.LoseHealth(damage);
                _health -= 2;
                if (_health <= 0)
                {
                    Instantiate(dieBloodParticle, this.transform.position, this.transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }
}
