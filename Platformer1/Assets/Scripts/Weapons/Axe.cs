﻿using UnityEngine;

public class Axe : MonoBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.gameObject.CompareTag("Player") && !hitInfo.gameObject.CompareTag("GUI"))
        {
            EnemyController enemyController = hitInfo.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.ApplyDamage(weapon.attack);
            }            
        }
    }
}
