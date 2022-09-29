using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timer = 0.0f;
    public static int seconds;
    private static bool _stop;

    [SerializeField]
    private Score scoreScript;
    public static bool stop
    {
        get
        {
            return _stop;
        }
        set
        {
            _stop = value;
        }
    }

    private TextMeshProUGUI _secondsText;
    // Start is called before the first frame update
    void Awake()
    {
        _stop = false;
        _secondsText = GetComponent<TextMeshProUGUI>();
        _secondsText.text = "TIME: 0";
    }

    private void Update()
    {
        if (_stop)
        {
            return;
        }

        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        _secondsText.text = "TIME: " + seconds;
        scoreScript.UpdateScore();
    }
}
