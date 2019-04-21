using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public Transform groundDetection;
    public float distance = 1;
    // force is how forcefully we will push the player away from the enemy.
    public float force = 30;

    private bool movingRight = true;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);

        if(groundInfo.collider == false)
        {
            if(movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(pushPlayer(other.gameObject));
            other.gameObject.SendMessage("applyDamage", 5.0f, SendMessageOptions.DontRequireReceiver);         
        }
    }

    private IEnumerator pushPlayer(GameObject player)
    {
        var dir = player.transform.position - transform.position;
        // normalize force vector to get direction only and trim magnitude
        dir.Normalize();
        player.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);
    }
}
