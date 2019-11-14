using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour {
   Player player;
   void Start() {
      player = GetComponentInParent<Player>();
   }

   void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Projectile") {
         player.TakeDamage(other.GetComponent<Projectile>().GetOwner().damage);
      }
   }
}
