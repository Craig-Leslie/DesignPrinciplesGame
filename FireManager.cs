using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireManager : MonoBehaviour
{

    public static FireManager instance;


    public TMP_Text firesLeft;
    public int fires = 6;

    public void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        firesLeft.text = ": " + fires.ToString(); 
    }

    
    public void lessFire() 
    {
        fires--;
        firesLeft.text = ": " + fires.ToString();
        if(fires == 0)
        {
            SceneManager.LoadScene(3);

        }

    }
}
