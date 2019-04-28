using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbsorbController : MonoBehaviour
{
    public ParticleSystem controlParticles;
    public Button absorbButton;

    private bool canControl = false;

    private GameObject otherPlayer = null;
    private GameController gameController;
    private Animator anim;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.Find("GameController").gameObject;

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        otherPlayer = null;
        anim = GetComponent<Animator>();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            absorbButton.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.25f, 10, 0.5f);
            canControl = true;
            otherPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            absorbButton.transform.DOScale(0f, 0.25f);
            canControl = false;
            otherPlayer = null;
        }
    }

    private void Update()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            if (CanControl() && Input.GetButtonDown("Absorb"))
            {
                ControlPlayer();
            }
        }
    }

    public void Absorb()
    {
        if (CanControl())
        {
            ControlPlayer();
        }
    }

    private bool CanControl()
    {
        return otherPlayer != null && canControl;
    }

    private void ControlPlayer()
    {
        if(otherPlayer != null)
        {
            anim.SetTrigger("absorb");

            // we instantiate the particles
            ParticleSystem absorbParticles = Instantiate(controlParticles, transform.position, transform.rotation, transform);
            Destroy(absorbParticles.gameObject, 2);                        

            gameController.ChangePlayer();

            // we deactivate my camera and player controller
            GameObject myCamera = transform.parent.Find("Camera").gameObject;            
            myCamera.gameObject.SetActive(false);

            GameController.ToggleScripts(gameObject, false);
            GameController.ToggleScripts(otherPlayer, true);            

            // we set active the other character's camera
            GameObject otherCamera = otherPlayer.transform.parent.Find("Camera").gameObject;            
            otherCamera.gameObject.SetActive(true);
        }
    }    
}
