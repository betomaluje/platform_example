using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Weapon weapon;
    private GameObject weaponObject;
    public SimpleHealthBar ammoBar;

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

    private void LateUpdate()
    {
        if (weapon != null && weapon.hasAmmoDisplay())
        {
            ammoBar.UpdateBar(currentAmmo, totalAmmo);
            ammoBar.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            ammoBar.transform.parent.gameObject.SetActive(false);
        }
    }

    private bool CanAttack()
    {
        bool timeEnable = Time.time > nextFire;

        bool enoughAmmo = currentAmmo > 0;

        if (weapon && weapon.weaponType == Weapon.Type.SHOOTING)
        {
            enoughAmmo = currentAmmo > 0;
        } else
        {
            enoughAmmo = true;
        }       

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
            nextFire = Time.time + weapon.rechargeTime;

            // we update the ammo
            currentAmmo--;

            // we attack
            switch (weapon.weaponType)
            {
                case Weapon.Type.SHOOTING:
                    ammoBar.UpdateBar(currentAmmo, totalAmmo);
                    StartCoroutine(weapon.ShootingAttack(weaponObject));
                    break;
                case Weapon.Type.MANUAL:
                    //StartCoroutine(weapon.ManualAttack(weaponObject));
                    weapon.SpecialAttack(weaponObject);
                    break;            
            }            
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
