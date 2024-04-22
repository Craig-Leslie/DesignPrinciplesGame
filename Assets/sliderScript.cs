using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{

    public Slider progbar;
    public GameObject Player;
    PlayerScript script;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Monkey");
        script = Player.GetComponent<PlayerScript>();
        progbar.minValue = 0f;
        progbar.maxValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        progbar.value = script.progression;
       


    }


}
