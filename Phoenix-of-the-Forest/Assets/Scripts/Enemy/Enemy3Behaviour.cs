using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Behaviour : BaseEnemeyBehaviour {
   EnemyActionMode mode;
   Player target;

   float attackTimer = 0.0f;

   // for enemies with projectiles only
   GameObject projectileInstance = null;

   new void Start() {
      //base.Start();
      health = enemy.maxHealth;
      mode = EnemyActionMode.Patrol;

      projectileInstance = Instantiate<GameObject>(enemy.projectileObject);
      projectileInstance.GetComponent<Projectile>().SetOwner(enemy);

      projectileInstance.SetActive(false);
   }

   new void Update() {
      if (mode == EnemyActionMode.Patrol) {
         Patrol();
      } else if (mode == EnemyActionMode.Aggressive) {
         MoveTowards(target);

         if (enemy.attackRange > Vector2.Distance(transform.position, target.transform.position) && attackTimer > enemy.attackSpeed) {
            Attack(target);
            attackTimer = 0.0f;
         }
      }
      attackTimer = Mathf.Min(attackTimer + Time.deltaTime, 1000.0f); // to prevent overflow
   }

   new void Attack(Player target) {
      Vector3 dir = target.transform.position - transform.position;
      dir.Normalize();

      projectileInstance.SetActive(true);
      projectileInstance.transform.position = transform.position;
      projectileInstance.GetComponent<Rigidbody2D>().velocity = (dir * enemy.projectileSpeed);
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

   IEnumerator DisableProjectile() {
      yield return new WaitForSeconds(3);
      projectileInstance.SetActive(false);
   }

   protected override void MoveTowards(Player target) {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      Vector3 velocity = Vector3.zero;
      direction = 1;
      if (target.transform.position.x < transform.position.x) direction = -1;

      int yDirection = 1;
      if (target.transform.position.y < transform.position.y) yDirection = -1;

      Vector2 targetVelocity;

      // Stop if we are close enough to attack
      if (Vector2.Distance(transform.position, target.transform.position) <= enemy.attackRange)
         targetVelocity = Vector2.zero;
      else {
         if (Mathf.Abs(transform.position.x - target.transform.position.x) < 1.0f) {
            targetVelocity = new Vector2(direction, yDirection);
         } else {
            targetVelocity = new Vector2(direction, 0);
         }

         targetVelocity = targetVelocity.normalized * enemy.moveSpeed;
      }

      isMoving = Mathf.Abs(targetVelocity.x) > 0.0f;



      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, enemy.movementSmoothing);
   }
}
