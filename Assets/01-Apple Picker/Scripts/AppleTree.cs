using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // speed at which the AppleTree moves
    private float[] speed = { 5f, 10f, 15f };

    // distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // chance that the AppleTree will change directions
    private float[] chanceToChangeDirections = { 0.02f, 0.05f, 0.08f };

    // rate at which apples will be instantiated
    private float[] secondsBetweenAppleDrops = { 1f, .8f, .5f };

    public static int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        // dropping apples every second
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops[level]);
    }

    // Update is called once per frame
    void Update()
    {
        // basic movement
        Vector3 pos = transform.position;
        pos.x += speed[level] * Time.deltaTime;
        transform.position = pos;

        //changing direction
        if (pos.x < -leftAndRightEdge)
        {
            speed[level] = Mathf.Abs(speed[level]); // Move right
        } else if (pos.x > leftAndRightEdge)
        {
            speed[level] = -Mathf.Abs(speed[level]); // Move left
        }
        
    }

    // time based... 50 per second
    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections[level])
        {
            speed[level] *= -1; // change direction
        }
    }
}
