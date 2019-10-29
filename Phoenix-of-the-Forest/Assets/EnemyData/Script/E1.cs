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
   
   public override void Patrol(Rigidbody2D rb) {
      float move =  moveSpeed;
      Vector3 velocity = Vector3.zero;
      Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
   }

   public override void Attack(Rigidbody2D rb, Transform target) {
      throw new System.NotImplementedException();
   }

   public override void MoveTowards(Rigidbody2D rb, Transform target) {
      throw new System.NotImplementedException();
   }
}
