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

    private void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        maxHealth = enemy.health;
        currentHealth = maxHealth;
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
            other.gameObject.SendMessage("applyDamage", enemy.attack, SendMessageOptions.DontRequireReceiver);
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

    public void applyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            ParticleSystem particles = Instantiate(damageParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, 1);
            // die
            Debug.Log("Enemy dead!");
            Destroy(gameObject);
        }

        healthBar.setHealth(currentHealth / maxHealth);

    }
}
