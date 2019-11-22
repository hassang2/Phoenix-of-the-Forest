using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuardianScript : MonoBehaviour {

   [SerializeField] GameObject champ;
   GameObject plat1;
   GameObject plat2;
   bool platX;
   bool enemyX;

   bool flipX = false;

   [SerializeField] float hoverOffest;
   Vector3 hoverOffestVector;

   // Start is called before the first frame update
   void Start() {
      platX = false;
      enemyX = false;
      hoverOffestVector = new Vector3(0, hoverOffest, 0);
   }

   // Update is called once per frame
   void Update() {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();

      Vector2 targetVelocity = champ.transform.position + hoverOffestVector - transform.position;
      rb.velocity = targetVelocity * 3.0f;

      flipX = transform.position.x < champ.transform.position.x;
      GetComponentInChildren<SpriteRenderer>().flipX = flipX;

      if (Input.GetMouseButtonDown(0) && !enemyX && !platX) {
         platX = true;
         plat1 = Instantiate(Resources.Load<GameObject>("Platform_Prefabs/platform_vines"));

         Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         plat1.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

         StartCoroutine(Kill());
      }
   }

   void OnTriggerEnter2D(Collider2D col) {
      if (col.gameObject.tag == "Enemy") {
         enemyX = true;
      }
   }

   private void OnTriggerExit2D(Collider2D col) {
      if (col.gameObject.tag == "Enemy") {
         enemyX = false;
      }
   }

   void OnTriggerStay2D(Collider2D col) {

      if (col.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0)) {
         col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
         StartCoroutine(Unfreeze(col));
      }



   }
   IEnumerator Kill() {
      yield return new WaitForSeconds(1.5f);
      plat2 = plat1;
      platX = false;
      yield return new WaitForSeconds(.5f);
      Destroy(plat2);
   }
   IEnumerator Unfreeze(Collider2D col) {
      yield return new WaitForSeconds(5);
      col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
      col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
   }
}
