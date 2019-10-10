using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour {
   [SerializeField] private float jumpForce = 600f;                   // Amount of force added when the player jumps.
   [Range(0, 3)]   [SerializeField] private float slideSpeedMult = 1.5f;         // Amount of maxSpeed applied to Slideing movement. 1 = 100%
   [Range(0, 1)]   [SerializeField] private float slideTimerMax = 0.2f;
   [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
   [SerializeField] private bool airControl = true;                     // Whether or not a player can steer while jumping;
   [SerializeField] private LayerMask whatIsGround;                   // A mask determining what is ground to the character
   [SerializeField] private Collider2D slideDisableCollider;            // A collider that will be disabled when Slideing
   [SerializeField] private int maxJumps = 2;

   private int curJumps = 0;
   const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
   private bool grounded;            // Whether or not the player is grounded.
   const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
   private Rigidbody2D rigidbody2D;
   private bool facingRight = true;  // For determining which way the player is currently facing.
   private Vector3 velocity = Vector3.zero;

   private float slideTimer = 0f;
   
   private Vector2 slideDir = new Vector2(1, 0);
   private bool isSliding = false;

   private Transform groundCheck;                    // A position marking where to check if the player is grounded.
   private Transform ceilingCheck;                   // A position marking where to check for ceilings

   private bool wasSliding = false;

   private void Awake() {
      rigidbody2D = GetComponent<Rigidbody2D>();
      groundCheck = transform.Find("GroundCheck");
      ceilingCheck = transform.Find("CeilingCheck");
   }

   private bool IsGrounded() {
      bool wasGrounded = grounded;

      // The player is grounded if a circlecast to the groundCheck position hits anything designated as ground
      // This can be done using layers instead but Sample Assets will not overwrite your project settings.
      Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
      for (int i = 0; i < colliders.Length; i++) {
         if (colliders[i].gameObject != gameObject) {
            return true;
         }
      }
      return false;
   }


   private void FixedUpdate() {
      grounded = IsGrounded();
   }


   public void Move(float move, bool jump, bool slide) {
      grounded = IsGrounded();

      // If Slideing, check to see if the character can stand up
      if (!slide) {
         // If the character has a ceiling preventing them from standing up, keep them Slideing
         if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
            slide = true;
         }
      } else if (!isSliding && grounded) {
         isSliding = true;
         slideTimer = 0f;
      }

      //only control the player if grounded or airControl is turned on
      if (grounded || airControl) {
         // If Slideing
         if (isSliding) {

            // Increase the speed by the slideSpeed multiplier
            move *= slideSpeedMult;
            slideTimer += Time.deltaTime;
            if (slideTimer > slideTimerMax) isSliding = false;


            // Disable one of the colliders when Slideing
            if (slideDisableCollider != null)
               slideDisableCollider.enabled = false;
         } else {
            // Enable the collider when not Slideing
            if (slideDisableCollider != null)
               slideDisableCollider.enabled = true;
         }

         // Move the character by finding the target velocity
         Vector3 targetvelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
         // And then smoothing it out and applying it to the character
         rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetvelocity, ref velocity, movementSmoothing);

         // If the input is moving the player right and the player is facing left...
         if (move > 0 && !facingRight) {
            // ... flip the player.
            Flip();
         }
         // Otherwise if the input is moving the player left and the player is facing right...
         else if (move < 0 && facingRight) {
            // ... flip the player.
            Flip();
         }
      }
      // If the player should jump...
      if (jump && curJumps < maxJumps) {
         // Add a vertical force to the player.
         grounded = false;
         rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
         rigidbody2D.AddForce(new Vector2(0f, jumpForce));
         curJumps++;
      }

      if (grounded) {
         curJumps = 0;
      }
   }


   private void Flip() {
      // Switch the way the player is labelled as facing.
      facingRight = !facingRight;

      // Multiply the player's x local scale by -1.
      Vector3 theScale = transform.localScale;
      theScale.x *= -1;
      transform.localScale = theScale;
   }
}
