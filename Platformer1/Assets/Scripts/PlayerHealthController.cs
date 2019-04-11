using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{

    public Slider healthBarSlider;  //reference for slider
    public float playerHealth = 100f;

    private bool isGameOver = false; //flag to see if game is over

    private void Start()
    {
        healthBarSlider.maxValue = playerHealth;
        healthBarSlider.value = playerHealth;
    }

    public void applyDamage(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0 )
        {
            Debug.Log("You dead!");
        } else
        {
            healthBarSlider.value = playerHealth;
        }
    }
}
