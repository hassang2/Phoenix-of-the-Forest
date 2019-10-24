using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

   [SerializeField]
   Enemy enemy;


   Rigidbody2D rb;

   void Awake() {
      rb = GetComponent<Rigidbody2D>();
   }

   void Start() {

   }

   void Update() {
      enemy.Move(rb);
   }
}
