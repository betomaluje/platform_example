using UnityEngine;
using System.Collections;

public class AbsorbController : MonoBehaviour
{
    public ParticleSystem controlParticles;

    private bool canControl = false;

    private GameObject otherPlayer = null;
    private PlayerController playerController;
    private GameController gameController;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.Find("GameController").gameObject;

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        playerController = gameObject.GetComponent<PlayerController>();
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
            // we instantiate the particles
            ParticleSystem absorbParticles = Instantiate(controlParticles, transform.position, Quaternion.identity, transform);
            Destroy(absorbParticles.gameObject, 2);

            gameController.ChangePlayer();

            // we deactivate my camera and player controller
            GameObject myCamera = transform.parent.Find("Camera").gameObject;

            playerController.enabled = false;
            myCamera.gameObject.SetActive(false);

            //Color colorToTurnTo = gameObject.GetComponent<Renderer>().material.color;
            Color colorToTurnTo = new Color(130, 211, 79);
            SpriteRenderer otherSprite =  otherPlayer.GetComponent<SpriteRenderer>();

            StartCoroutine(UpdateColor(otherSprite, colorToTurnTo));

            yield return null;

            // we set active the other character's camera
            GameObject otherCamera = otherPlayer.transform.parent.Find("Camera").gameObject;

            otherPlayer.GetComponent<PlayerController>().enabled = true;
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
