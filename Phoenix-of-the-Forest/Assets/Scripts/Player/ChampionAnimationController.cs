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
      anim.SetBool("isGrounded", playerMovement.grounded);

      anim.SetBool("isAttacking", player.isAttacking);

      anim.SetBool("isWalking", playerMovement.isWalking);
   }
}
