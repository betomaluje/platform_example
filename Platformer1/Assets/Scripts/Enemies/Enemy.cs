using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public LayerMask groundLayer;
    public float attack;
    public float impactForce;
    public float health;
    public float speed;
    public float groundDistance;
}
