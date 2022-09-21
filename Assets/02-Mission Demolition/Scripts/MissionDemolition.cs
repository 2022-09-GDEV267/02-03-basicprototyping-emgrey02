using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // a private Singleton

    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitHighScore;
    public Text uitButton;
    public Vector3 castlePos; // place to put castle
    public GameObject[] castles; // array of castles

    [Header("Set Dynamically")]
    public int level; // current level
    public int levelMax; // number of levels
    public int shotsTaken;
    public int[] highScore; // best num of shots on each level
    public GameObject castle; // current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode

    private void Awake()
    {
        highScore = new int[castles.Length];
        for (int i = 0; i < castles.Length; i++)
        {
            string name = "HighScore";
            name += i;
            if (PlayerPrefs.HasKey(name))
            {
                highScore[i] = PlayerPrefs.GetInt(name);
            }
            else
            {
                highScore[i] = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        S = this; // define Singleton


        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // get rid of old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        // destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // get a reference to the Building component of Main Camera
        Building bScript = Camera.main.GetComponent<Building>();

        // remove buildings and create new ones
        bScript.clearBuildings();
        bScript.createBuildings();

        // instantiate new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // reset the camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        // reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

    }

    void UpdateGUI()
    {
        // show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;

        if (highScore[level] == 0)
        {
            uitHighScore.text = "best score: N/A";
        } else
        {
            uitHighScore.text = "best score: " + highScore[level];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // check for level end
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            // change mode to stop checking for level end
            mode = GameMode.levelEnd;

            // zoom out
            SwitchView("Show Both");

            // start the next level in 2 seconds
            Invoke("NextLevel", 2f);

        }

    }

    void NextLevel()
    {
        string playerPrefsText1 = "HighScore";
        playerPrefsText1 += level;

        if (highScore[level] == 0)
        {
            highScore[level] = shotsTaken;
            PlayerPrefs.SetInt(playerPrefsText1, highScore[level]);
        } else if (highScore[level] > shotsTaken)
        {
            highScore[level] = shotsTaken;
            PlayerPrefs.SetInt(playerPrefsText1, highScore[level]);
        }
    
        level++;

        if (level == levelMax)
        {
            level = 0;
        }

        string playerPrefsText2 = "HighScore";
        playerPrefsText2 += level;

        if (PlayerPrefs.HasKey(playerPrefsText2)) {
            highScore[level] = PlayerPrefs.GetInt(playerPrefsText2);
        } 

        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }

        showing = eView;
        switch(showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
