using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public GameObject hook;
    public GameObject target;
    public float speed;
    public float retractSpeed;
    public float retractionDeathThreshold;
    bool retracting = false;

    void Update()
    {
        if(retracting)
        {

            if (Vector3.Distance(transform.position, hook.transform.position) > retractionDeathThreshold)
            {
                Destroy(hook);
                retracting = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                Instantiate(hook, transform.position, hook.transform.rotation);
            }

            if (Input.GetMouseButton(1))
            {

            }

            if (Input.GetMouseButtonUp(1))
            {
                retracting = true;
            }
        }
    }
}
