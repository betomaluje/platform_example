using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.CompareTag("Player") && !hitInfo.gameObject.CompareTag("GUI"))
        {
            EnemyController enemyController = hitInfo.GetComponent<EnemyController>();

            if(enemyController != null)
            {
                enemyController.applyDamage(weapon.attack);
            }

            // we destroy this bullet
            Destroy(gameObject);
        }       
    }
}
