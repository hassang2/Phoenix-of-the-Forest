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
   public int damage;
   public float moveSpeed;
   public float attackRange;
   public float attackSpeed; // attacks per second

   [SerializeField] protected float movementSmoothing = 0.0f;


   public abstract void Start();

   public abstract void MoveTowards(EnemyBehaviour eb, Player target);

   public abstract void Patrol(EnemyBehaviour rb);

   public abstract void Attack(EnemyBehaviour eb, Player target);
}
