using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1.25f);
    }
}
