using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActionMode {
   Static,
   Patrol,
   Aggressive,
};
public enum EnemyType {
   Melee,
   Ranged,
}

public abstract class Enemy : ScriptableObject {
   public int maxHealth;
   public int damage;
   public float moveSpeed;
   public float attackRange;
   public float attackSpeed; // attacks per second

   [SerializeField] public EnemyType type;

   [SerializeField] public float movementSmoothing = 0.0f;

   // for ranged enemies only:
   [SerializeField] public float projectileSpeed = 10.0f;
   [SerializeField] public GameObject projectileObject;
}
