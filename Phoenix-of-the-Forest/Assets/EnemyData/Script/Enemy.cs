using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyActionMode {
   Static,
   Patrol,
   Aggressive,
};
public abstract class Enemy : ScriptableObject {
   [SerializeField] protected float health;
   [SerializeField] protected float damage;
   [SerializeField] protected float moveSpeed;

   [SerializeField] protected float movementSmoothing = 0.0f;


   public abstract void Start();

   public abstract void MoveTowards(Rigidbody2D rb, Transform target);

   public abstract void Patrol(Rigidbody2D rb);

   public abstract void Attack(Rigidbody2D rb, Transform target);
}
