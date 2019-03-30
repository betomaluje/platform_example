using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int amountOfPlayers = 2;
    private int currentPlayers = 0;

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentPlayers--;
        //Debug.Log("exit currentPlayers " + currentPlayers);

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayers++;

            //Debug.Log("enter currentPlayers " + currentPlayers);

            if (currentPlayers == amountOfPlayers)
            {
                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
