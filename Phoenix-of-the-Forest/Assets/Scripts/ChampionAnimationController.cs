using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionAnimationController : MonoBehaviour {
   Animator anim;

   PlayerMovement playerMovement;
   Player player;

   void Start() {
      anim = GetComponent<Animator>();

      playerMovement = GetComponentInParent<PlayerMovement>();
      player = GetComponentInParent<Player>();
   }


   void Update() {
      bool grounded = playerMovement.IsGrounded();
      anim.SetBool("isGrounded", grounded);

      bool isAttacking = player.IsAttacking();
      anim.SetBool("isAttacking", isAttacking);

      bool isWalking = playerMovement.IsWalking();
      anim.SetBool("isWalking", isWalking);
   }
}
