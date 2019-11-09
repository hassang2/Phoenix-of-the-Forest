using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {
   // Start is called before the first frame update
   Player player;
   void Start() {
      player = GetComponentInParent<Player>();
   }

   // Update is called once per frame
   void Update() {

   }
   void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Projectile") {
         player.TakeDamage(other.GetComponent<Projectile>().GetOwner());
      }
   }
}
