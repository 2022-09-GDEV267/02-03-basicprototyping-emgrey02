using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // static point of interest
    static public GameObject POI;

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

        // force destination.z to be camZ to keep camera far enough away
        destination.z = camZ;

        // set the camera to the destination
        transform.position = destination;
    }

}
