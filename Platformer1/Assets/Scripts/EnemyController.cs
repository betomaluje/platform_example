using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public Transform groundDetection;
    public float distance = 1;

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
}
