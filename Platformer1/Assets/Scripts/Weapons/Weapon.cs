using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public string description;
    public int attack;
    public Type weaponType;
    
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
        Debug.Log("Shooting!");
        GameObject bullet = weaponObject.transform.Find("Bullet").gameObject;
        Debug.Log(bullet);
        if (bullet)
        {
            Debug.Log("there's a bullet!");
        }
    }

    private void ManualAttack(GameObject weaponObject)
    {
        Debug.Log("Manual!");
    }
}
