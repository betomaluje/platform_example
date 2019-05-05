using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public Transform groundDetection;
    public ParticleSystem damageParticles;

    private bool movingRight = true;
    private HealthBar healthBar;
    private float currentHealth;
    private float maxHealth;
    private CameraShake cameraShake;

    private void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        cameraShake = GetComponent<CameraShake>();
        maxHealth = enemy.health;
        currentHealth = maxHealth;
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * enemy.speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, enemy.groundDistance, enemy.groundLayer);

        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                movingRight = false;
            }
            else
            {
                transform.Rotate(0f, 0f, 0f);
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(pushPlayer(other.gameObject));
            PlayerStatsController playerStatsController = other.GetComponent<PlayerStatsController>();
            if (playerStatsController != null)
            {
                playerStatsController.applyDamage(enemy.attack);
            }
        }
    }

    private IEnumerator pushPlayer(GameObject player)
    {
        var dir = player.transform.position - transform.position;
        // normalize force vector to get direction only and trim magnitude
        dir.Normalize();
        player.GetComponent<Rigidbody2D>().AddForce(dir * enemy.impactForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);
    }

    public void ApplyDamage(float damage)
    {
        healthBar.gameObject.SetActive(true);
        currentHealth -= damage;

        if (cameraShake != null)
        {
            cameraShake.ShakeIt();
        }

        if (currentHealth <= 0)
        {
            ParticleSystem particles = Instantiate(damageParticles, transform.position, Quaternion.identity);           
            // die
            Destroy(gameObject);
        }

        healthBar.setHealth(currentHealth / maxHealth);
        StartCoroutine(HideHealthBar());
    }

    private IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(1f);
        healthBar.gameObject.SetActive(false);
    }
}
