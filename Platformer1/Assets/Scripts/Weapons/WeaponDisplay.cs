using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public Weapon weapon;
    public ParticleSystem burstParticles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // we instantiate the particles
            ParticleSystem absorbParticles = Instantiate(burstParticles, other.transform.position, Quaternion.identity);
            Destroy(absorbParticles.gameObject, 1);

            // we replace the game object of the player
            GameObject weaponObject = gameObject.transform.GetChild(0).gameObject;            

            GameObject weaponHolder = other.transform.Find("WeaponHolder").gameObject;

            // we move the game object to the player
            weaponObject.transform.parent = weaponHolder.transform;
            weaponObject.transform.position = weaponHolder.transform.position;
            weaponObject.transform.rotation = weaponHolder.transform.rotation;

            weaponHolder.GetComponentInParent<PlayerWeaponController>().updateWeapon(weapon, weaponObject);           

            Destroy(gameObject);
        }
    }
}
