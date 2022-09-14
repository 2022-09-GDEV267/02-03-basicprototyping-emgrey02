using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Basket : MonoBehaviour
{

    [Header("Set Dynamically")]

    public TextMeshProUGUI scoreGT;

    public int appleCount = 0;

    void Start()
    {
        // find reference to ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        // get text component of that GameObject
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();

        // set starting number of points to 0
        scoreGT.text = "0";

    }


    // Update is called once per frame
    void Update()
    {
        // get current screen position of mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // convert point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // move x position of basket to x position of mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        // find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            appleCount++;
        }

        // parse text of scoreGT into an int
        int score = int.Parse(scoreGT.text);

        // add points for catching the apple
        score += 100;

        // convert score back to string and display it
        scoreGT.text = score.ToString();

        // track the high score
        if (score > HighScore.score)
        {
            HighScore.score = score;
        }

        if (appleCount == 10)
        {
            appleCount = 0;
            if (AppleTree.level != 2)
            {
                AppleTree.level++;
            }
        }
    }
}
