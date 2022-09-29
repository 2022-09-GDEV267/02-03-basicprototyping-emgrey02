using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadApplePicker : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Main-ApplePicker");
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}
