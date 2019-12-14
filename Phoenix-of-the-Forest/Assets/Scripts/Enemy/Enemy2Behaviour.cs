using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behaviour : BaseEnemeyBehaviour {
   //[SerializeField] new E2 enemy;

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
      animationController = GetComponentInChildren<TomatoAnimationController>();
   }

   new void Update() {
      if (mode == EnemyActionMode.Patrol) {
         Patrol();
      } else if (mode == EnemyActionMode.Aggressive) {
         MoveTowards(target);

         if (enemy.attackRange > Vector2.Distance(transform.position, target.transform.position) && attackTimer > enemy.attackSpeed) {
            Attack(target);
            attackTimer = 0.0f;
            animationController.PlayAttack();
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

      //StartCoroutine(DisableProjectile());
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
