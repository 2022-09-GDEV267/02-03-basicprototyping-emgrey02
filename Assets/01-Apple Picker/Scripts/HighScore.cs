using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    static public int score;
    public TextMeshProUGUI highScore;

    void Awake()
    {
        // if the PlayerPrefs HighScore already exists, read it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }

        // assign high score to HighScore
        PlayerPrefs.SetInt("HighScore", score);
    }

    private void Start()
    {
        NewToggle.newHighScore = false;
    }
    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>();
        gt.text = "High Score: " + score;

        //update PlayerPrefs HighScore if necessary
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            NewToggle.newHighScore = true;

        }

    }

    void clearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
