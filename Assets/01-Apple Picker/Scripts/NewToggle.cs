using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewToggle : MonoBehaviour
{
    public static bool newHighScore;

    // Start is called before the first frame update
    void Start()
    {
        if (!newHighScore)
        {
            Destroy(this.gameObject);
        }
        
    }
}
