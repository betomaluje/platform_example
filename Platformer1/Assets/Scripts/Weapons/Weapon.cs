using UnityEngine;

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
    public float shootingTime;      
    
    public enum Type
    {
        SHOOTING, MANUAL
    }    

    public void Attack(GameObject weaponObject)
    {
        switch(weaponType)
        {
            case Type.SHOOTING:
                ShootingAttack(weaponObject);
                break;
            case Type.MANUAL:
                ManualAttack(weaponObject);
                break;
        }
    }

    private void ShootingAttack(GameObject weaponObject)
    {
        GameObject bullet = weaponObject.transform.Find("Bullet").gameObject;        

        if (bullet)
        {
            GameObject bulletInstance = Instantiate(bullet, weaponObject.transform.position, weaponObject.transform.rotation);
            bulletInstance.SetActive(true);

            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.velocity = weaponObject.transform.right * speed;
        }
    }

    private void ManualAttack(GameObject weaponObject)
    {
        Debug.Log("Manual!");
    }

    public void DropWeapon(GameObject weaponObject)
    {
        Destroy(weaponObject);
        Debug.Log("out of ammo!");
    }
}
