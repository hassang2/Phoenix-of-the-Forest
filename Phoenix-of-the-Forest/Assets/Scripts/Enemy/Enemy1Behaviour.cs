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
   protected override void MoveTowards(Player target) {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      Vector3 velocity = Vector3.zero;
      direction = 1;
      if (target.transform.position.x < transform.position.x) direction = -1;

      Vector2 targetVelocity;

      // Stop if we are close enough to attack
      if (Vector2.Distance(transform.position, target.transform.position) + 0.1f <= enemy.attackRange)
         targetVelocity = Vector2.zero;
      else
         targetVelocity = new Vector2(enemy.moveSpeed * direction, rb.velocity.y);

      
      // gives the object an upward jump in case its collider is stuck on tile colliders
      if (targetVelocity.magnitude > 0.05f && rb.velocity.magnitude < 0.05f)
         targetVelocity += new Vector2(0, 20f);

      isMoving = Mathf.Abs(targetVelocity.x) > 0.05f;


      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, enemy.movementSmoothing);
   }





         
}
