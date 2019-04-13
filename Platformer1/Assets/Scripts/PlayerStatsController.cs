using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{

    //public Slider healthBarSlider;  //reference for slider
    public SimpleHealthBar healthBar;
    public float playerHealth = 100f;

    public float speed = 60;
    public float jumpForce = 10;

    private bool isGameOver = false; //flag to see if game is over

    private float maxHealth;

    private void Start()
    {
        maxHealth = playerHealth;
        healthBar.UpdateBar(maxHealth, maxHealth);
    }

    public void applyDamage(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0 )
        {
            Debug.Log("You dead!");
        } else
        {
            healthBar.UpdateBar(playerHealth, maxHealth);
        }
    }

    public void applyHealth(float health)
    {
        float tempHealth = playerHealth + health;

        if(tempHealth > maxHealth)
        {
            tempHealth = maxHealth;
        }

        healthBar.UpdateBar(tempHealth, maxHealth);
    }
}
