using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool grabbed;
    private GameObject point;
    private void Update()
    {
        if(grabbed && point)
        {
            transform.position = point.transform.position;
        }
    }

    public void Grabbed(GameObject target)
    {
        point = target;
        grabbed = true;
    }
}
