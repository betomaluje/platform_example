using UnityEngine;
using System.Collections;

public class ForegroundAlphaController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color originalColor;
    private Color alphaColor;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        originalColor = sprite.color;
        alphaColor = originalColor;

        alphaColor.a = 0.25f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(UpdateColor(sprite, alphaColor));
        }
    }

    private IEnumerator UpdateColor(SpriteRenderer fadeimage, Color newColor)
    {
        float timer = 0.0f;
        float time = 0.25f;

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
