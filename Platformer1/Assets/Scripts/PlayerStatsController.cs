using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatsController : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public float playerHealth = 100f;
    public ParticleSystem damageParticles;
    public GameController gameController;
    public Image blood;

    public float speed = 60;
    public float jumpForce = 10;

    [Range(0, 1)]
    public float decaeDamage = 0.1f;

    private bool isGameOver = false; //flag to see if game is over

    private float maxHealth;
    private CameraShake cameraShake;

    private void Awake()
    {
        blood.canvasRenderer.SetAlpha(0.0f);
    }

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
            gameController.SetGameOver(true);
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
        Instantiate(damageParticles, transform.position, Quaternion.identity);        
        playerHealth -= damage;
        if(cameraShake != null)
        {
            cameraShake.ShakeIt();
        }

        if(Random.Range(0,3) == 2) {
            StartCoroutine(ShowBlood());
        }        
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

    public IEnumerator ShowBlood()
    {
        blood.CrossFadeAlpha(1.0f, 0.25f, false);
        yield return new WaitForSeconds(0.15f);
        blood.CrossFadeAlpha(0.0f, 0.5f, false);
    }
}
