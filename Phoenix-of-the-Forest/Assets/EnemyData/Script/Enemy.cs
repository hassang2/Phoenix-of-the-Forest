using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyActionMode {
   Static,
   Patrol,
   Aggressive,
};
public abstract class Enemy : ScriptableObject {
   public float maxHealth;
   public float damage;
   public float moveSpeed;
   public float attackRange;

   [SerializeField] protected float movementSmoothing = 0.0f;


   public abstract void Start();

   public abstract void MoveTowards(EnemyBehaviour eb, Transform target);

   public abstract void Patrol(EnemyBehaviour rb);

   public abstract void Attack(EnemyBehaviour eb, Transform target);
}
