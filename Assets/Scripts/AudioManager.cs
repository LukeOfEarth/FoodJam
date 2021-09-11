using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject[] sounds;

    private void FixedUpdate()
    {
        if(GameObject.Find("Fly") == null)
        {
            sounds[0].SetActive(false);
        }
        else
        {
            sounds[0].SetActive(true);
        }
    }
}
