using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
   [SerializeField] Enemy enemy;

   Rigidbody2D rb;
   EnemyActionMode mode;
   Transform target;


   void Start() {
      enemy.Start();
      mode = EnemyActionMode.Patrol;
      rb = GetComponent<Rigidbody2D>();
   }

   void Update() {
      if (mode == EnemyActionMode.Patrol) enemy.Patrol(rb);
      else if (mode == EnemyActionMode.Aggressive) {
         enemy.MoveTowards(rb, target);
         enemy.Attack(rb, target);
      }
   }


   void OnTriggerEnter2D(Collider2D other) {
      if (other.tag == "Player") {
         target = other.transform;
         mode = EnemyActionMode.Aggressive;
      }
   }
}
