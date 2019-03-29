using UnityEngine;

public class AbsorbController : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    bool canAbsorb = false;

    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.gameObject.CompareTag("Player"))
        {
            canAbsorb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent.gameObject.CompareTag("Player"))
        {
            canAbsorb = false;
        }
    }

    private void Update()
    {
        if(canAbsorb && Input.GetButtonDown("Absorb"))
        {
            Debug.Log("absorbing other player");
        }
    }
}
