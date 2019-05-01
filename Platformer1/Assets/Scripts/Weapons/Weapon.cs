using UnityEngine;
using System.Collections;
using DG.Tweening;

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

            weaponObject.transform.DOLocalRotate(new Vector3(0, 0, -90), .2f).SetEase(Ease.InOutSine);

            yield return new WaitForSeconds(0.1f);

            weaponObject.transform.DOLocalRotate(new Vector3(0, 0, 0), .2f).SetEase(Ease.InOutSine);

            isInUse = false;

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }       
    }

    public void SpecialAttack(GameObject weaponObject)
    {
        if(weaponType.Equals(Type.MANUAL))
        {
            // throw
            Axe axeScript = weaponObject.GetComponent<Axe>();
            if(axeScript != null)
            {
                axeScript.Throw();
            }
        }
    }

    public void DropWeapon(GameObject weaponObject)
    {
        Destroy(weaponObject);
        Debug.Log("out of ammo!");
    }

    public bool hasAmmoDisplay()
    {
        return weaponType.Equals(Type.SHOOTING);
    }
}
