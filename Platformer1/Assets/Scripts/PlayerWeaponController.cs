using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Weapon weapon;
    private GameObject weaponObject;

    private float nextFire;

    private int totalAmmo;
    private int currentAmmo;

    private void Awake()
    {
        weapon = null;
        weaponObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {           
            Attack();
        }
    }

    private bool CanAttack()
    {
        bool timeEnable = Time.time > nextFire;

        bool enoughAmmo = currentAmmo > 0;

        if(!enoughAmmo && weapon && weaponObject)
        {
            weapon.DropWeapon(weaponObject);
        }
             
        return (weapon && weaponObject && timeEnable && enoughAmmo);
    }

    public void Attack()
    {
        if (CanAttack()) {
            //we update the time
            nextFire = Time.time + weapon.shootingTime;

            // we update the ammo
            currentAmmo--;

            // we attack
            weapon.Attack(weaponObject);
        }
    }

    public void updateWeapon(Weapon newWeapon, GameObject newWeaponObject)
    {
        weapon = newWeapon;
        weaponObject = newWeaponObject;

        totalAmmo = weapon.ammo;
        currentAmmo = weapon.currentAmmo;
    }
}
