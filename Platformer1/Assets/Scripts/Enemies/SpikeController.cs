using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("applyDamage", 10.0f, SendMessageOptions.DontRequireReceiver);
        }
    }
}
