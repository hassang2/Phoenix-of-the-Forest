using UnityEngine;

public class Corruption : MonoBehaviour {
   [SerializeField] float speed = 1.0f;
   void Update() {
      GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
   }

   void OnTriggerEnter2D(Collider2D other) {
      if(other.name == "HitBox") { 
         if (other.transform.parent.tag == "Player" || other.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponentInParent<Player>().TakeDamage(10000);
         }
      }
   }
}
