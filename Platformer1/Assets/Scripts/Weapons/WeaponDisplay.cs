using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public Weapon weapon;
    public ParticleSystem burstParticles;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = weapon.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // we instantiate the particles
            ParticleSystem absorbParticles = Instantiate(burstParticles, other.transform.position, Quaternion.identity);
            Destroy(absorbParticles.gameObject, 1);

            // we replace the game object of the player
            other.transform.Find("WeaponHolder").GetComponent<SpriteRenderer>().sprite = weapon.sprite;
            other.transform.Find("WeaponHolder").parent.GetComponent<PlayerController>().updateWeapon(weapon);

            Destroy(gameObject);
        }
    }
}
