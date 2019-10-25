using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
   E1 enemy;


   Rigidbody2D rb;

   void Awake() {
      rb = GetComponent<Rigidbody2D>();
      enemy = new E1();
   }

   void Start() {

   }

   void Update() {
      enemy.Move(rb);
   }
}
