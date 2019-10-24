using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1 : Enemy {
   public override void Move(Rigidbody2D rb) {
      float move = 1.0f;
      Vector3 velocity = Vector3.zero;
      Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
   }

   public override void Attack() {
      throw new System.NotImplementedException();
   }
}
