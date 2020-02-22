using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI Score;

    private string timeTracker;
    private string timeAdd = "s";

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
            timeTracker = Time.timeSinceLevelLoad.ToString("F2");

        }
        Score.text = timeTracker + timeAdd;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timeTracker = "0";
        Score.text = "0";
        timeAdd = "0";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
