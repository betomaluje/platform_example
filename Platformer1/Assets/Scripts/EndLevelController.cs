using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevelController : MonoBehaviour
{
    public ParticleSystem controlParticles;
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        // we instantiate the particles
        ParticleSystem endParticles = Instantiate(controlParticles, transform.position, Quaternion.identity, transform);        

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
