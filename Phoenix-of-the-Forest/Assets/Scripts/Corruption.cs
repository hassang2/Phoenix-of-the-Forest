using UnityEngine;

public class Corruption : MonoBehaviour {
   [SerializeField] float speed = 1.0f;
   [SerializeField] GameObject player;


   void Start() {
      GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
      transform.position = player.transform.position - new Vector3(8.0f, 0, 0);
   }

   void Update() {
      float distance = player.transform.position.x - transform.position.x;

      if (distance > 20.0f)
         GetComponent<Rigidbody2D>().velocity = new Vector2(speed * 4, 0);
      else if (distance > 12.0f)
         GetComponent<Rigidbody2D>().velocity = new Vector2(speed * 2, 0);
      else
         GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
   }

   void OnTriggerEnter2D(Collider2D other) {
      if(other.name == "HitBox") { 
         if (other.transform.parent.tag == "Player" || other.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponentInParent<Player>().TakeDamage(1000);
         }
      }
   }
}
