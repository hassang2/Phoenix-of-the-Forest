using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{

    Transform t;
    bool on;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        on = true;
    }

    public void Freeze()
    {
        on = false;
        StartCoroutine(Wait());
    }

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        on = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            t.rotation = t.rotation * Quaternion.Euler(0, 0, 5);
        }
           
    }
}
