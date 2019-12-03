using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behaviour : BaseEnemeyBehaviour {

   EnemyActionMode mode;
   Player target;

   float attackTimer;
   SnakeAnimationController animationController;

   new void Start() {
      base.Start();
      health = enemy.maxHealth;
      mode = EnemyActionMode.Patrol;

      attackTimer = 0.0f;

      animationController = GetComponentInChildren<SnakeAnimationController>();
   }

   new void Update() {
      if (mode == EnemyActionMode.Patrol) {
         Patrol();
      } else if (mode == EnemyActionMode.Aggressive) {
         MoveTowards(target);

         if (enemy.attackRange >= Vector2.Distance(transform.position, target.transform.position) && attackTimer > enemy.attackSpeed) {
            Attack(target);
            attackTimer = 0.0f;
            animationController.PlayAttack();

         }
      }
      attackTimer = Mathf.Min(attackTimer + Time.fixedDeltaTime, 1000.0f); // to prevent overflow
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
