using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public string description;
    public int attack;
    public float speed;
    public Type weaponType;
    public int ammo;
    public int currentAmmo;

    // in seconds
    public float rechargeTime;

    private bool isInUse = false;
    
    public enum Type
    {
        SHOOTING, MANUAL
    }    

    public IEnumerator ShootingAttack(GameObject weaponObject)
    {
        GameObject bullet = weaponObject.transform.Find("Bullet").gameObject;        

        if (bullet)
        {
            GameObject bulletInstance = Instantiate(bullet, weaponObject.transform.position, weaponObject.transform.rotation);
            bulletInstance.SetActive(true);

            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.velocity = weaponObject.transform.right * speed;
        }

        yield return null;
    }

    public IEnumerator ManualAttack(GameObject weaponObject)
    {
        if(!isInUse)
        {            
            PlayerController playerController = weaponObject.GetComponentInParent<PlayerController>();
            if(playerController != null)
            {
                playerController.enabled = false;
            }

            isInUse = true;

            Quaternion from = weaponObject.transform.rotation;
            Quaternion to = weaponObject.transform.rotation;
            Vector3 v = from.eulerAngles;

            to *= Quaternion.Euler(0,0, - 90);

            float elapsed = 0.0f;
            float duration = rechargeTime;

            while (elapsed < duration)
            {
                weaponObject.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);            
                elapsed += Time.deltaTime;
                yield return null;
            }
            weaponObject.transform.rotation = to;

            // now back
            elapsed = 0.0f;
            while (elapsed < duration)
            {
                weaponObject.transform.rotation = Quaternion.Slerp(to, from, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            weaponObject.transform.rotation = from;
            isInUse = false;

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }

        
    }

    public void DropWeapon(GameObject weaponObject)
    {
        Destroy(weaponObject);
        Debug.Log("out of ammo!");
    }
}
