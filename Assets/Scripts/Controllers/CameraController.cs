using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = player;
        vcam.LookAt = player;
        Debug.Log(vcam.Follow);
        Debug.Log("setting player vcam: " + player);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
