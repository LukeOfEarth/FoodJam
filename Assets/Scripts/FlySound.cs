using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySound : MonoBehaviour
{
    public float threshold;
    public float d;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        d = Vector2.Distance(this.transform.position, player.position);
        if (d < threshold)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
