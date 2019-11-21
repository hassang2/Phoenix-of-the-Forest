using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioClip[] clips = new AudioClip[3];
    AudioSource source;
    PlayerMovement player;

    bool inAir;
    


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = GetComponentInParent<PlayerMovement>();
        inAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        JumpSFX();
        AttackSFX();
        
    }
    void WalkSFX()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Debug.Log(2);
            source.PlayOneShot(clips[0]);
        }
    }

    void JumpSFX()
    {
        if (Input.GetButtonDown("Jump") && player.IsGrounded())
        {
            source.PlayOneShot(clips[1]);
        }
        else if (Input.GetButtonDown("Jump") && !player.IsGrounded() && !inAir)
        {
            source.PlayOneShot(clips[2]);
            inAir = true;
        }

        if (inAir && player.IsGrounded())
        {
            inAir = false;
        }
    }

    void AttackSFX()
    {
        if(Input.GetButtonDown("Attack") && player.IsGrounded())
        {
            source.PlayOneShot(clips[3]);
        }
        else if(Input.GetButtonDown("Attack") && !player.IsGrounded())
        {
            source.PlayOneShot(clips[4],.2f);
        }
    }
}
