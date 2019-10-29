using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/E1")]
public class E1 : Enemy {

   public override void Start() {
      //health = 100.0f;
      //damage = 5.0f;
      //moveSpeed = 1.0f;
   }
   
   public override void Patrol(EnemyBehaviour eb) {
      // TODO
      // The following code only moves the enemy in one direciton
      //Rigidbody2D rb = eb.GetComponent<Rigidbody2D>();

      //float move =  moveSpeed;
      //Vector3 velocity = Vector3.zero;
      //Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
      //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
   }

   public override void Attack(EnemyBehaviour eb, Transform target) {
      throw new System.NotImplementedException();
   }

   public override void MoveTowards(EnemyBehaviour eb, Transform target) {
      Rigidbody2D rb = eb.GetComponent<Rigidbody2D>();
      Vector3 velocity = Vector3.zero;
      int direction = 1;
      if (target.position.x < eb.transform.position.x) direction = -1;

      Vector2 targetVelocity = Vector3.zero;

      // Stop if we are close enough to attack
      if (Vector2.Distance(eb.transform.position, target.position) <= attackRange) targetVelocity = Vector2.zero;
      else targetVelocity = new Vector2(moveSpeed * 10f * direction, rb.velocity.y);

      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

      Debug.Log("here");
   }
}
