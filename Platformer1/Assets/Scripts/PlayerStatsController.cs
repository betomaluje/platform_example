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

    public void applyDamage(float damage)
    {
        playerHealth -= damage;
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
