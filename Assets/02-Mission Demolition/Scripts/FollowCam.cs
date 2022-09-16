using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // static point of interest
    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    // desired Z pos of the camera
    public float camZ;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        if (POI == null) return;

        // get position of poi
        Vector3 destination = POI.transform.position;

        // limit x & y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        // interpolate from current camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        // force destination.z to be camZ to keep camera far enough away
        destination.z = camZ;

        // set the camera to the destination
        transform.position = destination;

        // set orthographicSize of the Camera to keep Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }

}
