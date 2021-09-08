using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites;
    void Awake()
    {
        int i = Random.Range(0, sprites.Length - 1);
        print(i);
        GetComponent<SpriteRenderer>().sprite = sprites[i];
    }
}
