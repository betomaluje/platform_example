using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public Weapon weapon;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = weapon.sprite;
    }   
}
