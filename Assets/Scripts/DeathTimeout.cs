using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimeout : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(!player.GetComponentInChildren<GrapplingGun>().active && !player.GetComponentInChildren<GrapplingGun>().retracting)
        {
            Destroy(this.gameObject);
        }
    }
}
