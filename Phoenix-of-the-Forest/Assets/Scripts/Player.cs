using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
   float damage;
   int health;

   void Start() {
      damage = 10000.0f;
      health = 6;
   }

   void Update() {

   }

   public void Attack() {
      Debug.Log("ATTACK");
   }

   public void TakeDamage(Enemy attacker) {
      health -= attacker.damage;
   }
}
