using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressSpaceToStart : MonoBehaviour
{
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine("Flash");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("SceneManager").GetComponent<Scenes>().ToMenu();
        }
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.5f);
        img.enabled = !img.enabled;
        StartCoroutine("Flash");
    }
}
