using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimeout : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine("FailSafe");
    }

    private void Update()
    {
        if(!player.GetComponentInChildren<GrapplingGun>().active && !player.GetComponentInChildren<GrapplingGun>().retracting)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponentInChildren<GrapplingGun>().active = false;
        player.GetComponentInChildren<GrapplingGun>().retracting = false;
    }
}
