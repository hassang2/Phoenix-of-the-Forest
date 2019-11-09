using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
   float damage;
   [SerializeField] PlayerHealth health;

   [SerializeField] int maxHealth = 6;
   [SerializeField] float defaultDamage = 10000.0f;
   void Start() {
      damage = defaultDamage;
      health.SetValue(maxHealth);
   }

   void Update() {

   }

   public void Attack() {
      Debug.Log("ATTACK");
   }

   public void TakeDamage(Enemy attacker) {
      health.AddValue(-attacker.damage);
   }
   
}
