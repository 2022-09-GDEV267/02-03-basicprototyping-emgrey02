using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float _scoreValue = 0;
    private TextMeshProUGUI _scoreText
    {
        get
        {
            return GetComponent<TextMeshProUGUI>();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        _scoreValue = Timer.seconds - Points.pointsValue;
        _scoreText.text = "SCORE: " + _scoreValue;
    }
}
