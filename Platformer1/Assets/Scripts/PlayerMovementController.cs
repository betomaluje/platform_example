using UnityEngine;

public abstract class PlayerMovementController : MonoBehaviour
{
    protected PlayerStatsController playerStats;
    protected float horizontalMove = 0;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerStats.speed; // -1 is left
    }
}
