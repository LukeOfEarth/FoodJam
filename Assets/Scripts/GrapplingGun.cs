using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;
    public GameObject grappleEnd;
    public GameObject collisionObj;
    private PlayerController playerController;

    [Header("Retraction:")]
    private GameObject contactPoint;
    public bool active = false;
    public bool retracting = false;
    public float retractionSpeed;
    public float retractionFinishDistance;
    public float retractionDelay;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] private LayerMask HitMe;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !active)
        {
            SetGrapplePoint();
            active = true;
        }
        else if (Input.GetKey(KeyCode.Mouse1) && !retracting)
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && !retracting)
        {
            Retract();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);

            if(retracting && active)
            {
                contactPoint.transform.position = Vector3.Lerp(contactPoint.transform.position, transform.position, retractionSpeed * Time.deltaTime);
                grapplePoint = new Vector2(contactPoint.transform.position.x, contactPoint.transform.position.y);
            }
        }
    }

    GameObject Retract()
    {
        contactPoint = Instantiate(grappleEnd, new Vector3(grapplePoint.x, grapplePoint.y, 0), transform.rotation);
        retracting = true;

        if (collisionObj)
        {
            switch (collisionObj.tag)
            {
                case "Food":
                    StartCoroutine("CompleteRetractFood");
                    break;
                case "Terrain":
                    StartCoroutine("CompleteRetractWall");
                    break;
            }
        }
        else
        {
            StartCoroutine("CompleteRetractWall");
        }
        return contactPoint;
    }

    void CompleteRetraction()
    {
        GameObject[] grapples = GameObject.FindGameObjectsWithTag("GrapplePoint");
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = playerController.baseGravity;
        foreach(GameObject grapple in grapples)
        {
            Destroy(grapple);
        }
        active = false;
        retracting = false;
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized, HitMe))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, HitMe);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                DecideCollisionBehaviour(_hit.transform.gameObject);
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    void DecideCollisionBehaviour(GameObject col)
    {
        collisionObj = col;
        switch (col.tag)
        {
            case "Food":
                StartCoroutine("GrabFood");
                break;
            case "Terrain":
                break;
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

    IEnumerator GrabFood()
    {
        yield return new WaitForSeconds(retractionDelay);
        GameObject newObj = Retract();
        collisionObj.GetComponent<Food>().Grabbed(newObj);
    }

    IEnumerator CompleteRetractWall()
    {
        yield return new WaitForSeconds(0.1f);
        CompleteRetraction();
    }

    IEnumerator CompleteRetractFood()
    {
        yield return new WaitForSeconds(0.2f);
        CompleteRetraction();
    }
}
