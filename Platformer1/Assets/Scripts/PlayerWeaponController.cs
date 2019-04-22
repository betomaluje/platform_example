using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Weapon weapon;
    private GameObject weaponObject;

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
        return (weapon && weaponObject);
    }

    public void Attack()
    {
        if (CanAttack()) {
            // we attack
            weapon.Attack(weaponObject);
        }
    }

    public void updateWeapon(Weapon newWeapon, GameObject newWeaponObject)
    {
        weapon = newWeapon;
        weaponObject = newWeaponObject;
    }
}
