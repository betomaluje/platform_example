using UnityEngine;

public class Axe : MonoBehaviour
{
    public Weapon weapon;
    public float rotationSpeed;

    private bool activated = false;

    private void Update()
    {
        if (activated)
        {
            transform.localEulerAngles += Vector3.right * rotationSpeed * Time.deltaTime;
        }
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

        activated = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
