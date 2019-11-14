using UnityEngine;

public class PlayerInput : MonoBehaviour {

   PlayerMovement playerMovement;
   Player player;
   float horizontalMove;

   bool jump = false;
   bool slide = false;

   void Awake() {
      playerMovement = GetComponent<PlayerMovement>();
      player = GetComponent<Player>();
   }
   void Update() {
      horizontalMove = Input.GetAxisRaw("Horizontal");

      if (Input.GetButtonDown("Jump")) {
         jump = true;
      } else if (Input.GetButtonDown("Slide")) {
         slide = true;
      } else if (Input.GetButtonDown("Attack")) {
         player.Attack();
      }

   }

   void FixedUpdate() {

      playerMovement.Move(horizontalMove * Time.fixedDeltaTime);
      bool didSlide = playerMovement.Slide(slide);
      bool didJump = playerMovement.Jump(jump);
      //player.Attack();

      if (jump) jump = false;
      if (slide) slide = false;
   }
}
