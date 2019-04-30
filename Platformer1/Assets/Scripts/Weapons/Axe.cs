using UnityEngine;

public class Axe : MonoBehaviour
{
    public Weapon weapon;
    public float rotationSpeed;

    private bool activated = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);
    }

    private void Update()
    {
        if (activated)
        {
            transform.localEulerAngles += Vector3.right * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        activated = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.CompareTag("Player") && !hitInfo.gameObject.CompareTag("GUI"))
        {
            EnemyController enemyController = hitInfo.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.ApplyDamage(weapon.attack);
            }            
        }
    }

    public void Throw()
    {
        Debug.Log("Throwing axe!");

        // we activate the rotation
        activated = false;

        // now we throw it
        rb.isKinematic = false;
        rb.transform.parent = null;
        rb.AddForce(transform.right * 12, ForceMode2D.Impulse);
    }
}
