using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemeyBehaviour : MonoBehaviour, IDamagable {
   [SerializeField] protected Enemy enemy;
   protected int health;
   protected void Start() {

   }

   // Update is called once per frame
   protected void Update() {

   }

   protected virtual void MoveTowards(Player target) {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      Vector3 velocity = Vector3.zero;
      int direction = 1;
      if (target.transform.position.x < transform.position.x) direction = -1;

      Vector2 targetVelocity;

      // Stop if we are close enough to attack
      if (Vector2.Distance(transform.position, target.transform.position) <= enemy.attackRange) targetVelocity = Vector2.zero;
      else targetVelocity = new Vector2(enemy.moveSpeed * direction, rb.velocity.y);

      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, enemy.movementSmoothing);
   }

   public virtual void Patrol() {

   }

   public void Attack(Player target) {
      target.TakeDamage(enemy.damage);
   }

   public virtual void TakeDamage(int amount) {
      health -= amount;
      if (health <= 0) {
         Destroy(gameObject);
      }
   }
}
