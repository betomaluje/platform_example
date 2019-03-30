using UnityEngine;

public abstract class PlayerMovementController : MonoBehaviour
{
    public float runSpeed = 40f;
    protected float horizontalMove = 0; 

    // Update is called once per frame
    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // -1 is left
    }
}
