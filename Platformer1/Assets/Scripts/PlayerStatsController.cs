using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public float playerHealth = 100f;
    public ParticleSystem damageParticles;

    public float speed = 60;
    public float jumpForce = 10;

    [Range(0, 1)]
    public float decaeDamage = 0.1f;

    private bool isGameOver = false; //flag to see if game is over

    private float maxHealth;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GetComponent<CameraShake>();
        maxHealth = playerHealth;
        healthBar.UpdateBar(maxHealth, maxHealth);
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("You dead!");
        }
        else
        {
            healthBar.UpdateBar(playerHealth, maxHealth);
        }
    }

    public void applyDecaeDamage()
    {
        playerHealth -= decaeDamage;
    }

    public void applyDamage(float damage)
    {
        ParticleSystem absorbParticles = Instantiate(damageParticles, transform.position, Quaternion.identity, transform);
        Destroy(absorbParticles.gameObject, 2);
        playerHealth -= damage;
        cameraShake.ShakeIt();
    }

    public void applyHealth(float health)
    {
        float tempHealth = playerHealth + health;

        if(tempHealth > maxHealth)
        {
            tempHealth = maxHealth;
        }

        playerHealth = tempHealth;
    }
}
