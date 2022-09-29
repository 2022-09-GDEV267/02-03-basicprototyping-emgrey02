using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform launchPos;
    public GameObject ballPrefab;

    public int count;

    [SerializeField]
    private TextMeshProUGUI ammoUsed;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(ballPrefab, launchPos.position, transform.rotation);
            count++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        ammoUsed.text = "SHOTS: " + count;
    }
}
