using UnityEngine;

public class PlayerAttackBox : MonoBehaviour {
    Player player;
    AudioSource aud;

    void Start() {

        player = GetComponentInParent<Player>();
        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "HitBox" && other.transform.parent.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            other.GetComponentInParent<BaseEnemeyBehaviour>().TakeDamage(player.GetDamageValue());
            aud.Play();

        }
    }
}
