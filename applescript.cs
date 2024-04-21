using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class applescript : MonoBehaviour
{

    public GameObject Player;
    PlayerScript script;
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Monkey");
        script = Player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        activated = script.appleActivated;
        if (activated)
        {

            transform.Translate(Vector3.up * -6f * Time.deltaTime);
        }
    }
}
