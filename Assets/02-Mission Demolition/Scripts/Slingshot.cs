using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    private void Awake()
    {
        // get the transform of a child of Slingshot: LaunchPoint
        Transform launchPointTrans = transform.Find("LaunchPoint");
        // gets the gameObject associated with that transform and applies it to the gameObject
        launchPoint = launchPointTrans.gameObject;
        // ignore launchpoint until mouse enters its area
        launchPoint.SetActive(false);

        // set launch position
        launchPos = launchPointTrans.position;
    }
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        // player has pressed mouse button over Slingshot
        aimingMode = true;

        // instantiate a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;

        // start projectile at launchPoint
        projectile.transform.position = launchPos;

        // set projectileRigidbody to isKinematic
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    private void Update()
    {
        // if slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return;

        // get current mouse position in 2d screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // find delta from launchPos to mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;

        // limit mouseDelta to radius of Slignshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            // Normalize() sets mouseDelta to 1 but keeps it pointing in the same direction
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // move projectile to new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0) )
        {
            // the mouse has been released
            aimingMode = false;
            // allow it to move due to velocity and gravity
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            // set cam to follow this projectile
            FollowCam.POI = projectile;
            // doesn't delete instance, just opens up field so can be filled by another instance
            projectile = null;
        }
    }
}
