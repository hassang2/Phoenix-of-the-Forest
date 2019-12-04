using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianFreeze : MonoBehaviour
{

    bool enemyx;
    Collider2D freezeBox;

    // Start is called before the first frame update
    void Start()
    {
        enemyx = false;


    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<GuardianScript>().enemyX = enemyx;

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && enemyx)
        {
            GetComponentInParent<GuardianTimer>().Freeze();

            freezeBox.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(GetComponentInParent<GuardianScript>().Unfreeze(freezeBox));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            enemyx = true;
            freezeBox = col;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            enemyx = false;
        }
    }
}
