using UnityEngine;

public class Projectile : MonoBehaviour {
   Enemy owner;

   public void SetOwner(Enemy e) {
      owner = e;
   }

   public Enemy GetOwner() {
      return owner;
   }

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.layer == LayerMask.NameToLayer("Platform") || other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
         transform.gameObject.SetActive(false);
      }
   }

}
