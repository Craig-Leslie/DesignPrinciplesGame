using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScaryPlayerScript : MonoBehaviour
{
    public Vector2 turn;

    public Animator anim;

    public TMP_Text fireText;
    
    public bool canMove = true;

    public bool nearFire;

    public GameObject curFire;

    public float charMoveSpeed = 3f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        fireText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            forward();
            setSpeed();
            setMotionSpeed();
            runSpeed();
        }

        if (!Input.anyKey){
            rb.velocity = Vector3.zero;
        }
        turn.x += Input.GetAxis("Mouse X");
        
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        if (Input.GetKeyDown(KeyCode.E) && nearFire)
        {
            canMove = false;
            Debug.Log("2");
            anim.SetBool("FireOut", true);
            StartCoroutine(fireOut());
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            curFire = collision.gameObject;
            nearFire = true;
            fireText.enabled = true;

        }

    }

    private void OnTriggerExit(Collider collision)
    {

        fireText.enabled = false;
        curFire = null;
        nearFire = false;

    }

    IEnumerator fireOut()
    {

        yield return new WaitForSeconds(5);
        curFire.SetActive(false);
        anim.SetBool("FireOut", false);
        fireText.enabled = false;
        FireManager.instance.lessFire();
        canMove = true;


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
