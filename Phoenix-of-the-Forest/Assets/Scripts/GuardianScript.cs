using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuardianScript : MonoBehaviour
{
    public Transform guard;

    GameObject plat1;
    GameObject plat2;
    bool platX;
    bool enemyX;

    // Start is called before the first frame update
    void Start()
    {
        platX = false;
        enemyX = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        guard.position = Vector2.Lerp(guard.position, pos, .5f);

        if (Input.GetMouseButtonDown(0) && !enemyX && !platX)
        {
            
            platX = true;
            plat1 = Instantiate(Resources.Load<GameObject>("Platform_Prefabs/platform_vines"));
            plat1.transform.position = guard.position;
            StartCoroutine(Kill());

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            enemyX = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            enemyX = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Enemy" && Input.GetMouseButtonDown(0))
        {
            col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(Unfreeze(col));
        }
           
        
        
    }
    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1.5f);
        plat2 = plat1;
        platX = false;
        yield return new WaitForSeconds(.5f);
        Destroy(plat2);
    }
    IEnumerator Unfreeze(Collider2D col)
    {
        yield return new WaitForSeconds(5);
        col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        col.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
