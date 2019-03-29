using UnityEngine;
using System.Collections;

public class AbsorbController : MonoBehaviour
{
    private CircleCollider2D circleCollider;

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
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
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
        if(CanControl() && Input.GetButtonDown("Absorb"))
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
            Debug.Log("absorbing other player " + otherPlayer.name);

            gameController.ChangePlayer();

            // we deactivate my camera and player controller
            GameObject myCamera = transform.parent.Find("Camera").gameObject;

            playerController.enabled = false;
            myCamera.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.25f);

            // we set active the other character's camera
            GameObject otherCamera = otherPlayer.transform.parent.Find("Camera").gameObject;

            otherPlayer.GetComponent<PlayerController>().enabled = true;
            otherCamera.gameObject.SetActive(true);
        }
    }

}
