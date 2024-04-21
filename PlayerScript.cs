using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public float charMoveSpeed = 3f;

    public Vector2 turn;

    public Animator anim;

    public VariableDeclarations variables;
    public bool canMove = true;
    
    public bool hasDrank = false;
    public bool drinkAnim = false;
    public TMP_Text drinkingtext;
    public AudioSource drinkingAudio;


    public bool hasSat = false;
    public bool sitAnim = false;
    public TMP_Text sitingtext;
    public AudioSource tv;
    public ParticleSystem tvPart;

    public bool hasSat2 = false;
    public bool sitAnim2 = false;
    public TMP_Text sittingText2;
    public AudioSource pageTurn;

    public bool hasTree = false;
    public bool treeAnim = false;
    public TMP_Text TreeText;
    public bool appleActivated = false;
    public AudioSource eatingAudio;

    public bool hasSlept = false;
    public bool sleepAnim = false;
    public TMP_Text SleepText;
    public AudioSource snore;

    public TMP_Text getUp;

    public TMP_Text dayOver;

    public float progression = 0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        drinkingtext.enabled = false;
        sitingtext.enabled = false;
        sittingText2.enabled = false;
        TreeText.enabled = false;
        SleepText.enabled = false;
        getUp.enabled = false;
        dayOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (progression == 1f)
        {
            dayOver.enabled = true;
        }

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
        //turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        if (Input.GetKeyDown(KeyCode.Space) && (anim.GetBool("Sitting") || anim.GetBool("Sitting2")))
        {
            if (anim.GetBool("Sitting"))
            {
                anim.SetBool("Sitting", false);
                anim.SetBool("Standing", true);

                tvPart.Stop();
                tv.Stop();
                transform.position = new Vector3(-1.5f, 0.119f, -1.694f);
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                canMove = true;
                progression += 0.25f;

            }

            else 
            {
                anim.SetBool("Sitting2", false);
                anim.SetBool("Standing", true);
                transform.position = new Vector3(1f, 0.119f, -1.4f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                pageTurn.Stop();
                canMove = true;
                progression += 0.25f;

            }
            getUp.enabled = false;

        }

        if (Input.GetKeyDown(KeyCode.E) && drinkAnim && !hasDrank)
        {
            hasDrank = true;
            Debug.Log("Collision2");
            drinkingtext.enabled = false;

            canMove = false;
            drinkingAudio.Play();
            anim.SetBool("Drinking", true);
            StartCoroutine(AnimationWait());
        }

        if (Input.GetKeyDown(KeyCode.E) && sitAnim && !hasSat)
        {
            anim.SetBool("Standing", false);
            hasSat = true;
            sitingtext.enabled = false;

            canMove = false;
            transform.position = new Vector3(-0.674f, 0.709f, -1.694f);
            transform.rotation = Quaternion.Euler(-32.119f, -90f, 0f);
            anim.SetBool("Sitting", true);
            tvPart.Play();
            tv.Play();
            getUp.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && sitAnim2 && !hasSat2)
        {
            anim.SetBool("Standing", false);

            hasSat2 = true;

            sitingtext.enabled = false;
            StartCoroutine(pageTurnLoop());
            canMove = false;
            transform.position = new Vector3(0.9984202f, 0.9341789f, -2.09449f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            anim.SetBool("Sitting2", true);
            getUp.enabled = true;

        }

        if (Input.GetKeyDown(KeyCode.E) && treeAnim && !hasTree)
        {
            hasTree = true;
            canMove = false;
            appleActivated = true;
            anim.SetBool("Eating", true);
            StartCoroutine(EatWait());

        }

        if (Input.GetKeyDown(KeyCode.E) && sleepAnim && !hasSlept)
        {
            hasSlept = true;
            canMove = false;
            transform.position = new Vector3(2.233f, 0.015f, -0.421f);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            anim.SetBool("Sleeping", true);
            StartCoroutine(sceneChange());
        }


        IEnumerator pageTurnLoop()
        {
            while (sitAnim2)
            {
                pageTurn.Play();
                yield return new WaitForSeconds(5);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision1");
            if (collision.gameObject.CompareTag("coffee") && !hasDrank)
            {
                drinkingtext.enabled = true;
                drinkAnim = true;
            }

            if (collision.gameObject.CompareTag("seat") && !hasSat)
            {
                sitingtext.enabled = true;
                sitAnim = true;
            }

            if (collision.gameObject.CompareTag("seat2") && !hasSat2)
            {
                sittingText2.enabled = true;
                sitAnim2 = true;
            }

            if (collision.gameObject.CompareTag("tree") && !hasTree)
            {
                TreeText.enabled = true;
                treeAnim = true;
            }

            if (collision.gameObject.CompareTag("bed") && !hasSlept && progression == 1f)
            {
            SleepText.enabled = true;
            sleepAnim = true;
            }
        

    }

    private void OnTriggerExit(Collider collision)
    {
        drinkAnim = false;
        drinkingtext.enabled = false;

        sitAnim = false;
        sitingtext.enabled = false;

        sitAnim2 = false;
        sittingText2.enabled = false;

        treeAnim = false;
        TreeText.enabled = false;

        sleepAnim = false;
        SleepText.enabled = false;

    }

    IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(7f);
        anim.SetBool("Drinking", false);
        canMove = true;
        hasDrank = true;
        progression += 0.25f;

    }

    IEnumerator EatWait()
    {
        yield return new WaitForSeconds(5f);
        eatingAudio.Play();

        yield return new WaitForSeconds(4f);

        anim.SetBool("Eating", false);
        canMove = true;
        hasTree = true;
        progression += 0.25f;

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

    IEnumerator sceneChange()
    {
        yield return new WaitForSeconds(5f);

        snore.Play();
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(2);
    }
}
