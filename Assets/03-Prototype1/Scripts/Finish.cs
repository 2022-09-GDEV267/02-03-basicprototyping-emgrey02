using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    public GameObject particles;

    [SerializeField]
    private Canvas canvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // set the alpha of the color of higher opacity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;

            Instantiate(particles, transform.position, particles.transform.rotation);
            Timer.stop = true;
            CreateEndScreen();
        }
    }

    void CreateEndScreen()
    {
        TextMeshProUGUI score = canvas.GetComponent<TextMeshProUGUI>();
        print(score.text);
    }
}
