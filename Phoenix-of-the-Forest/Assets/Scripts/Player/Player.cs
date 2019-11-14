using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {
   int damage;
   [SerializeField] PlayerHealth health;

   [SerializeField] int maxHealth = 6;
   [SerializeField] int defaultDamage = 10000;
   [SerializeField] float attackTime = 0.4f; // How long the player attackBox stays active after pressing attack

   bool isAttacking; // whether or not the player attackbox is active
   GameObject colliderObject;
   void Start() {
      damage = defaultDamage;
      health.SetValue(maxHealth);
      isAttacking = false;

      colliderObject = transform.Find("AttackCollider").gameObject;
      colliderObject.SetActive(false);
   }

   public void Attack() {
      if (isAttacking) return;

      isAttacking = true;
      colliderObject.SetActive(true);
      StartCoroutine(DisableCollider());
   }

   public void TakeDamage(int amount) {
      health.AddValue(-amount);
   }

   public int GetDamageValue() {
      return damage;
   }

   IEnumerator DisableCollider() {
      yield return new WaitForSeconds(attackTime);
      colliderObject.SetActive(false);
      isAttacking = false;
   }
}
