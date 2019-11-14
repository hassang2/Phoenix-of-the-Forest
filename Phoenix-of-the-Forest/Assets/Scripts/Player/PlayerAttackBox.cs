using UnityEngine;

public class PlayerAttackBox : MonoBehaviour {
   Player player;
   void Start() {
      player = GetComponentInParent<Player>();
   }

   void OnTriggerEnter2D(Collider2D other) {
      if (other.name == "HitBox" && other.transform.parent.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
         other.GetComponentInParent<BaseEnemeyBehaviour>().TakeDamage(player.GetDamageValue());
      }
   }
}
