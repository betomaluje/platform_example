using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatsController playerStatsController = other.GetComponent<PlayerStatsController>();
            if (playerStatsController != null)
            {
                playerStatsController.applyDamage(10.0f);
            }
        }
    }
}
