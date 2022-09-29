using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    public Obstacle obstacle;
   
    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(obstacle.health / obstacle.maxHealth, 0f, 1f);
    }
}
