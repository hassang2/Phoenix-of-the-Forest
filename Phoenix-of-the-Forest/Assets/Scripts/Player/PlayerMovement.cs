using UnityEngine;

public class PlayerMovement : MonoBehaviour {
   [SerializeField] float jumpForce = 600f;                   // Amount of force added when the player jumps.
   [SerializeField] int maxJumps = 2;

   [Range(0, 3)] [SerializeField] float slideSpeedMult = 1.5f;         // Amount of maxSpeed applied to Slideing movement. 1 = 100%
   [Range(0, 1)] [SerializeField] float slideTimerMax = 0.2f;
   [Range(0.0f, 2.0f)] [SerializeField] float slideCooldown = 0.7f;
   [SerializeField] Collider2D slideDisableCollider;            // A collider that will be disabled when Slideing

   [Range(0, .3f)] [SerializeField] float movementSmoothing = .05f; // How much to smooth out the movement
   [SerializeField] float moveSpeed = 40.0f;

   

   [SerializeField] bool airControl = true;                     // Whether or not a player can steer while jumping;
   [SerializeField] LayerMask whatIsGround;                   // A mask determining what is ground to the character
   [SerializeField] LayerMask whatIsWall;
   
   int curJumps = 0;

   const float collisionRadius = .2f; // Radius of the overlap circle to determine if grounded
   bool grounded;            // Whether or not the player is grounded.

   bool touchingRightWall;
   bool touchingLeftWall;
   Vector2 wallJumpDir = new Vector2(-2, 1);     // wall jumping direction to the left. multiply x by -1 for left
   float wallJumpForceMult = 2.0f;

   const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
   new Rigidbody2D rigidbody2D;
   bool facingRight = true;  // For determining which way the player is currently facing.
   
   float speedMod = 1.0f;

   GameObject weapon;

   float timeSinceLastSlide = 0.0f;
   float slideTimer = 0f;
   bool isSliding = false;

   Transform groundCheck;                    // A position marking where to check if the player is grounded.
   Transform ceilingCheck;                   // A position marking where to check for ceilings
   Transform rightWallCheck;
   Transform leftWallCheck;

   void Awake() {
      wallJumpDir.Normalize();

      rigidbody2D = GetComponent<Rigidbody2D>();
      groundCheck = transform.Find("GroundCheck");
      ceilingCheck = transform.Find("CeilingCheck");

      rightWallCheck = transform.Find("RightWallCheck");
      leftWallCheck  = transform.Find("LeftWallCheck");
   }

   bool IsGrounded() {
      // The player is grounded if a circlecast to the groundCheck position hits anything designated as ground
      // This can be done using layers instead but Sample Assets will not overwrite your project settings.
      Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, collisionRadius, whatIsGround);
      for (int i = 0; i < colliders.Length; i++) {
         if (colliders[i].gameObject != gameObject) {
            return true;
         }
      }
      return false;
   }

   // checks if we are touching any walls from left or right AND sets the values of rightWallCheck and leftWallCheck
   void CheckTouchingWall() {
      touchingRightWall = false;
      touchingLeftWall  = false;

      if (grounded) return;

      Collider2D[] colliders = Physics2D.OverlapCircleAll(rightWallCheck.position, collisionRadius, whatIsWall);
      for (int i = 0; i < colliders.Length; i++) {
         if (colliders[i].gameObject != gameObject) {
            touchingRightWall = true;
            
         }
      }

      colliders = Physics2D.OverlapCircleAll(leftWallCheck.position, collisionRadius, whatIsWall);
      for (int i = 0; i < colliders.Length; i++) {
         if (colliders[i].gameObject != gameObject) {
            touchingLeftWall = true;
         }
      }
   }

   void FixedUpdate() {
      CheckTouchingWall();

      if (grounded) {
         curJumps = 0;
      }
      
   }

   public void Move(float move) {
      grounded = IsGrounded();
      //only control the player if grounded or airControl is turned on
      if (grounded || airControl) {
         move *= moveSpeed;
         move *= speedMod;

         // Move the character by finding the target velocity
         Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
         // Apply smoothing
         Vector3 velocity = Vector3.zero;
         rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

         // flip the sprite
         if ((move > 0 && !facingRight) || (move < 0 && facingRight)) {
            Flip();
         }
      }
   }

   public bool Slide(bool shouldSlide) {
      bool didSlide = false;
      // If Slideing, check to see if the character can stand up
      if (!shouldSlide) {
         // If the character has a ceiling preventing them from standing up, keep them Slideing
         if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
            shouldSlide = true;
         }
      } else if (!isSliding && grounded) {
         if (timeSinceLastSlide >= slideCooldown) {
            isSliding = true;
            slideTimer = 0f;
            timeSinceLastSlide = 0;
            speedMod *= slideSpeedMult;
         }
      }

      if (isSliding) {
         // Increase the speed by the slideSpeed multiplier
         if (slideTimer > slideTimerMax) {
            isSliding = false;
            speedMod /= slideSpeedMult;
         }

         // Disable one of the colliders when Slideing
         if (slideDisableCollider != null) {
            slideDisableCollider.enabled = false;
         } else {
            // Enable the collider when not Slideing
            if (slideDisableCollider != null) {
               slideDisableCollider.enabled = true;
            }
         }

         slideTimer += Time.deltaTime;
         didSlide = true;
      }

      timeSinceLastSlide += Time.deltaTime;
      timeSinceLastSlide = Mathf.Min(timeSinceLastSlide, 100); // to prevent overflow

      return didSlide;
   }

   public bool Jump(bool shouldJump) {
      bool didJump = false;
      // If the player should jump...
      if (shouldJump && curJumps < maxJumps) {
         // Add a vertical force to the player.
         grounded = false;
         rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
         if (touchingRightWall) {
            Vector2 dir = wallJumpDir * jumpForce * wallJumpForceMult;
            rigidbody2D.AddForce(dir);
         } else if (touchingLeftWall) {
            Vector2 dir = new Vector2(wallJumpDir[0] * -1, wallJumpDir[1]);
            rigidbody2D.AddForce(jumpForce * dir * wallJumpForceMult);
         } else {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
         }
         
         curJumps++;
         didJump = true;
      }

      return didJump;
   }

   // Flip the sprite of the player
   private void Flip() {
      // Switch the way the player is labelled as facing.
      facingRight = !facingRight;

      //Vector3 newScale = transform.Find("Sprite").localScale;
      //newScale.x *= -1;
      //transform.Find("Sprite").localScale = newScale;
      transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = !transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
   }


}