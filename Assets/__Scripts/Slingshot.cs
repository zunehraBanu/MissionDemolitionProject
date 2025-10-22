using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
    public bool flag = true;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;
    public LineRenderer slingLine; // assign in Inspector

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    private Vector3 slingBandLeft;   // left side of the band
    private Vector3 slingBandRight;  // right side of the band
    [Header("Effects")]
    public GameObject blockHitEffectPrefab;


    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    void Awake()
    {
        S = this;

        // Find launch point
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        // Set left and right posts of the slingshot
        slingBandLeft = launchPos + new Vector3(-0.2f, 0, 0);
        slingBandRight = launchPos + new Vector3(0.2f, 0, 0);

        // Setup Line Renderer
        slingLine.positionCount = 3; // left, projectile, right
        slingLine.useWorldSpace = true;

        // Collapse line initially
        slingLine.SetPosition(0, slingBandLeft);
        slingLine.SetPosition(1, slingBandLeft);
        slingLine.SetPosition(2, slingBandRight);
    }

    void OnMouseEnter() => launchPoint.SetActive(true);
    void OnMouseExit() => launchPoint.SetActive(false);

    void OnMouseDown()
{
    if (!flag) return;

    aimingMode = true;

    // Instantiate the projectile
    projectile = Instantiate(prefabProjectile);
    projectile.transform.position = launchPos;

    // Set it kinematic so it doesn't move yet
    projectileRigidbody = projectile.GetComponent<Rigidbody>();
    projectileRigidbody.isKinematic = true;

    // Assign particle effect to the projectile
    Projectile projScript = projectile.GetComponent<Projectile>();
    if (projScript != null)
    {
        projScript.blockHitEffect = blockHitEffectPrefab;
    }
}


    void Update()
    {
        // Update rubber band if aiming
        if (aimingMode && projectile != null)
        {
            slingLine.SetPosition(0, slingBandLeft);
            slingLine.SetPosition(1, projectile.transform.position);
            slingLine.SetPosition(2, slingBandRight);
        }

        if (!aimingMode || !flag) return;

        // Mouse position in world space
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Calculate delta and limit to sphere radius
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move projectile with mouse
        projectile.transform.position = launchPos + mouseDelta;

        // Fire projectile
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            flag = false;

            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
	    GetComponent<AudioSource>().Play();
            FollowCam.POI = projectile;
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;

            projectile = null;

            // Collapse the slingshot line
            slingLine.SetPosition(0, slingBandLeft);
            slingLine.SetPosition(1, slingBandLeft);
            slingLine.SetPosition(2, slingBandRight);

            // Reset flag after 3 seconds
            Invoke("ResetFlag", 3f);
        }
    }

    void ResetFlag()
    {
        flag = true;
    }
}
