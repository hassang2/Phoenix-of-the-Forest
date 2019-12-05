using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoAnimationController : AnimationController {
   bool facingRight;

   bool isAttacking;

   void Start() {
      anim = GetComponent<Animator>();

      enemy = GetComponentInParent<Enemy2Behaviour>();
      facingRight = true;
      isAttacking = false;
   }

   // Update is called once per frame
   void Update() {
      anim.SetBool("isMoving", enemy.isMoving);
      anim.SetBool("isAttacking", isAttacking);

      if ((enemy.direction == 1 && facingRight) || (enemy.direction == -1 && !facingRight)) {
         GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
         facingRight = !facingRight;
      }
   }

   public override void PlayAttack() {
      isAttacking = true;
      StartCoroutine(DisableAttackAnimation());
   }

   // sets isAttacking to false after a short delay after attacking so the animation controller can render the correct sprite
   IEnumerator DisableAttackAnimation() {
      yield return new WaitForSeconds(0.2f);
      isAttacking = false;
   }
}
