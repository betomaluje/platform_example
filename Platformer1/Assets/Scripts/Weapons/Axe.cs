using UnityEngine;

public class Axe : MonoBehaviour
{
    public Weapon weapon;
    public float rotationSpeed;
    public LayerMask layerMask;

    private Rigidbody2D rb;
    private bool isBeingThrown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBeingThrown)
        {
            transform.localEulerAngles += Vector3.back * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (isBeingThrown && (layerMask & 1 << hitInfo.gameObject.layer) == 1 << hitInfo.gameObject.layer)
        {
            // hit something so we need to stuck it there
            isBeingThrown = false;
            rb.bodyType = RigidbodyType2D.Static;
        }

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
        // we activate the rotation
        isBeingThrown = true;

        // now we throw it
        rb.transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(transform.right * 15, ForceMode2D.Impulse);
    }
}
