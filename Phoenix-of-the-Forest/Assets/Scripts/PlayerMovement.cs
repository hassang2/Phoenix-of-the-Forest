using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

   public CharacterController2D controller;


   [SerializeField]
   float runSpeed = 40f;
   float horizontalMove = 0f;

   bool jump = false;

   // The duration the jump singal will be active once a jump button is pressed
   //[SerializeField]
   [SerializeField]
   [Range(0.0f, 1.0f)]
   float jump_active_threshhold = 0.1f;

   float jump_btn_timer = 0f;

   // Update is called once per frame
   void Update() {
      horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

      if (Input.GetButtonDown("Jump")) {
         jump = true;
      }

   }

   void FixedUpdate() {
      controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
      Debug.Log(jump_btn_timer);
       
      if (jump_btn_timer > 0.1) {
         jump = false;
         jump_btn_timer = 0f;
      } else if (jump) {
         jump_btn_timer += Time.fixedDeltaTime;
      }
   }
}
