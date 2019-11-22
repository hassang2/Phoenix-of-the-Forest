using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionAnimationController : MonoBehaviour {
   Animator anim;

   void Start() {
      anim = GetComponent<Animator>();
   }


   void Update() {
      bool grounded = GetComponentInParent<PlayerMovement>().IsGrounded();
      anim.SetBool("isGrounded", grounded);
   }
}
