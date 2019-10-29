using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
   [SerializeField] Enemy enemy;

   EnemyActionMode mode;
   Player target;

   float health;

   void Start() {
      enemy.Start();
      health = enemy.maxHealth;
      mode = EnemyActionMode.Patrol;
   }

   void Update() {
      if (mode == EnemyActionMode.Patrol) {
         enemy.Patrol(this);
      } else if (mode == EnemyActionMode.Aggressive) {
         enemy.MoveTowards(this, target);
         enemy.Attack(this, target);
      }
   }


   // Won't work if the player is already in the trigger area
   void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Player") {
         target = other.GetComponent<Player>();
         mode = EnemyActionMode.Aggressive;

         // Cheap trick to ignore collision between player and enemy
         Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<BoxCollider2D>());
         Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());
      }
   }
}
