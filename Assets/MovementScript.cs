using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MovementScript : MonoBehaviour
{
    public float charMoveSpeed = 3f;
    public Rigidbody rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        forward();
        setSpeed();
        setMotionSpeed();
        runSpeed();
        if (!Input.anyKey)
        {
            rb.velocity = Vector3.zero;
            anim.SetFloat("MotionSpeed", 0f);

        }
    }

    void forward()
    {
        float y = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward;
        Vector3 newForward = forward * y;

        rb.velocity = newForward * charMoveSpeed;
    }

    void runSpeed()
    {
        float y = Input.GetAxis("Vertical");

        anim.SetFloat("MotionSpeed", y);
        anim.SetFloat("Speed", Math.Abs(2f * y));
    }

    void setSpeed()
    {
        anim.SetFloat("Speed", 2f);

    }
    void setMotionSpeed()
    {
        anim.SetFloat("MotionSpeed", 1f);

    }

    void OnFootstep()
    {

    }
}
