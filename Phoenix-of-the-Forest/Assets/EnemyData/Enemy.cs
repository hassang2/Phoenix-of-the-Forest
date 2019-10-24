using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : ScriptableObject {
   public float health;
   public float damage;
   public float moveSpeed;

   [Range(0, .3f)] [SerializeField] public float movementSmoothing = .05f; // How much to smooth out the movement


   public abstract void Move(Rigidbody2D rb);
   public abstract void Attack();
}
