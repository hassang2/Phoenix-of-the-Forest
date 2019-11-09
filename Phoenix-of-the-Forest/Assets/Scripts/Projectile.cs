using UnityEngine;

public class Projectile : MonoBehaviour {
   Enemy owner;
   void Start() {

   }

   // Update is called once per frame
   void Update() {

   }

   public void SetOwner(Enemy e) {
      owner = e;
   }

   public Enemy GetOwner() {
      return owner;
   }

   private void OnTriggerEnter(Collider other) {
      Debug.Log("YEET");
      if (other.tag == "Player") {
         other.gameObject.GetComponent<Player>().TakeDamage(owner);
      }
   }

}
