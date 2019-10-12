using UnityEngine;

public class PlayerInput : MonoBehaviour {

   Player player;

   float horizontalMove;

   bool jump = false;
   bool slide = false;

   void Awake() {
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
      
      player.Move(horizontalMove * Time.fixedDeltaTime);
      bool didSlide = player.Slide(slide);
      bool didJump = player.Jump(jump);
      //player.Attack();

      if (jump) jump = false;
      if (slide) slide = false;
   }
}
