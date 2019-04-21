using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public LayerMask groundLayer;
    public int attack;
    public float impactForce;
    public int health;
    public float speed;
    public float groundDistance;
}
