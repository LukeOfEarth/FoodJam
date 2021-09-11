using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    private AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
