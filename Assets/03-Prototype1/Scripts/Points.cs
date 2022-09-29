using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Points : MonoBehaviour
{
    public static float pointsValue = 0;
    private TextMeshProUGUI _pointsText;

    public GameObject obstacles;

    // Start is called before the first frame update
    void Awake()
    {
        _pointsText = GetComponent<TextMeshProUGUI>();
        UpdateScore(pointsValue);
        SetupListeners();
    }

    void SetupListeners()
    {
        foreach (Transform obstacle in obstacles.transform)
        {
            if (obstacle.GetComponent<Obstacle>() != null)
            {
                obstacle.GetComponent<Obstacle>().OnDestroy += UpdateScore;
            }
        }
    }

    public void UpdateScore(float value)
    {
        pointsValue += value;
        _pointsText.text = "POINTS: " + pointsValue;
    }
}
