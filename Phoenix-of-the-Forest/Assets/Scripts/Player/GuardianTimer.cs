using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardianTimer : MonoBehaviour
{



    [SerializeField] Image freeImg;
    [SerializeField] Image platImg;

    float freeTimer;
    float platTimer;

    // Start is called before the first frame update
    void Start()
    {
        freeTimer = 5;
        platTimer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (freeTimer <= 5)
        {
            freeTimer += Time.deltaTime;
            freeImg.fillAmount = (float).5 * (freeTimer / 5);
        }

        if (platTimer <= 1.5f)
        {
            platTimer += Time.deltaTime;
            platImg.fillAmount = (float).5 * (platTimer / 1.5f);
        }
        
    }

    public void Freeze()
    {
        freeTimer = 0;
    }

    public void Platform()
    {
        platTimer = 0;
    }
}
