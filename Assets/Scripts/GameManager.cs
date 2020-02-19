using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text Score =null;
    
    private string timeTracker;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!DestroyBuggy.isDead)
        {
            timeTracker = Time.time.ToString("F2");
         
        }
        Debug.Log(timeTracker);
        Debug.Log(PlayerController.carSpeed.ToString("F0"));
        Score.text = timeTracker;

    }
}
