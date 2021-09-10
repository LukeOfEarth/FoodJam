using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("DeathTimer");
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
