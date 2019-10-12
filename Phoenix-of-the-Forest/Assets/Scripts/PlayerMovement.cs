using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

   public CharacterController2D controller;

   
   [SerializeField] float runSpeed = 40f;


   float horizontalMove = 0f;

   bool jump = false;
   bool slide = false;

   // The duration the jump singal will be active once a jump button is pressed
   //[SerializeField]
   [SerializeField]
   [Range(0.0f, 1.0f)]
   float jumpActiveThreshhold = 0.1f;

   float jumpBtnTimer = 0f;

   void Update() {
      horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

      if (Input.GetButtonDown("Jump")) {
         jump = true;
      } else if (Input.GetButtonDown("Slide")) {
         slide = true;
      }

   }

   void FixedUpdate() {
      
      controller.Move(horizontalMove * Time.fixedDeltaTime);
      controller.Slide(slide);
      controller.Jump(jump);

      if (jump) jump = !jump;
      if (slide) slide = !slide;
      
      //if (jumpBtnTimer > 0.1) {
      //   jump = false;
      //   jumpBtnTimer = 0f;
      //} else if (jump) {
      //   jumpBtnTimer += Time.fixedDeltaTime;
      //}
   }
}
