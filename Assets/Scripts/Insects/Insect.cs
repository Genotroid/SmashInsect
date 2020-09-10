using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InsectMover))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Insect : MonoBehaviour
{
    [SerializeField] protected int Health = 1;
    [SerializeField] protected int ScoreReward = 1;
}