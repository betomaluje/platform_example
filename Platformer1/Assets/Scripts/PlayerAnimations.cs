using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void AnimationIsRunning(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
    }

    public void AnimationAbsorb()
    {
        anim.SetTrigger("absorb");
    }

}
