using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    public GameObject music;

    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("MenuMusic") == null)
        {
            Instantiate(music);
        }

        if(GameObject.FindGameObjectWithTag("GameMusic") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("GameMusic"));
        }
    }
}
