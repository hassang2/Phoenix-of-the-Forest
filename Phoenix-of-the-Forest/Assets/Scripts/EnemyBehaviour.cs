using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
   [SerializeField] Enemy enemy;

   EnemyActionMode mode;
   Player target;

   float health;
   float attackTimer = 0.0f;

   void Start() {
      enemy.Start();
      health = enemy.maxHealth;
      mode = EnemyActionMode.Patrol;
   }

   void Update() {
      if (mode == EnemyActionMode.Patrol) {
         enemy.Patrol(this);
      } else if (mode == EnemyActionMode.Aggressive) {
         enemy.MoveTowards(this, target);

         if (enemy.attackRange > Vector2.Distance(transform.position, target.transform.position) && attackTimer > enemy.attackSpeed) {
            enemy.Attack(this, target);
            attackTimer = 0.0f;
         }
      }
      attackTimer = Mathf.Min(attackTimer + Time.deltaTime, 1000.0f); // to prevent overflow
   }


   // Won't work if the player is already in the trigger area
   void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Player") {
         target = other.GetComponent<Player>();
         mode = EnemyActionMode.Aggressive;

         // Cheap trick to ignore collision between player and enemy
         Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<BoxCollider2D>());
         Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());
      }
   }
}
