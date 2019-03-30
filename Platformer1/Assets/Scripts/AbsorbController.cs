using UnityEngine;
using System.Collections;

public class AbsorbController : MonoBehaviour
{
    public ParticleSystem controlParticles;

    private CircleCollider2D circleCollider;

    private bool canControl = false;

    private GameObject otherPlayer = null;
    private PlayerMovementController playerController;
    private GameController gameController;
    private CharacterController2D characterController;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.Find("GameController").gameObject;

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        playerController = gameObject.GetComponent<PlayerMovementController>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        characterController = gameObject.GetComponent<CharacterController2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canControl = true;
            otherPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canControl = false;
            otherPlayer = null;
        }
    }

    private void Update()
    {
        if (CanControl() && Input.GetButtonDown("Absorb"))
        {
            StartCoroutine(ControlPlayer());
        }
    }

    private bool CanControl()
    {
        return playerController.enabled && canControl;
    }

    IEnumerator ControlPlayer()
    {
        if(otherPlayer != null)
        {
            // if we are facing left, we need to flip the particle system
            if (characterController != null && !characterController.IsFacingRight())
            {
                // Multiply the player's x local scale by -1.
                Vector3 theScale = controlParticles.transform.localScale;
                theScale.x *= -1;
                controlParticles.transform.localScale = theScale;
            }

            // we instantiate the particles
            ParticleSystem particles = Instantiate(controlParticles, transform.position, Quaternion.identity, transform);
            Destroy(particles, 1);

            gameController.ChangePlayer();

            // we deactivate my camera and player controller
            GameObject myCamera = transform.parent.Find("Camera").gameObject;

            playerController.enabled = false;
            myCamera.gameObject.SetActive(false);

            //Color colorToTurnTo = gameObject.GetComponent<Renderer>().material.color;
            Color colorToTurnTo = new Color(0, 196, 18);
            SpriteRenderer otherSprite =  otherPlayer.GetComponent<SpriteRenderer>();

            StartCoroutine(UpdateColor(otherSprite, colorToTurnTo));

            yield return new WaitForSeconds(0.25f);

            // we set active the other character's camera
            GameObject otherCamera = otherPlayer.transform.parent.Find("Camera").gameObject;

            otherPlayer.GetComponent<PlayerMovementController>().enabled = true;
            otherCamera.gameObject.SetActive(true);
        }
    }

    private IEnumerator UpdateColor(SpriteRenderer fadeimage, Color newColor)
    {
        float timer = 0.0f;
        float time = 1.0f;

        Color oldColor = fadeimage.color;

        while (timer <= time)
        {
            timer += Time.deltaTime;
            float lerp_Percentage = timer / time;

            fadeimage.color = Color.Lerp(oldColor, newColor, lerp_Percentage);

            yield return null;
        }
    }
}
