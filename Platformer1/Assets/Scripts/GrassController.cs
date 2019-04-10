using System.Collections;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    public float rotateAmount = 20f; // Amount to be rotated

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RotateMe(Vector3.forward * rotateAmount));
        }
    }

    // This will handle when the colliding object leaves the plant or grass bit
    void OnTriggerExit2D(Collider2D other)
    {
        // Check that the colliding object is the player object
        if (other.gameObject.tag == "Player") 
        { 
            StartCoroutine(RotateMe(-Vector3.forward * rotateAmount));
        }
    }

    IEnumerator RotateMe(Vector3 byAngles)
    {
        float timer = 0.0f;
        float time = 0.1f;

        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

        while (timer <= time)
        {
            timer += Time.deltaTime;
            float lerp_Percentage = timer / time;

            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, lerp_Percentage);

            yield return null;
        }
    }
}
