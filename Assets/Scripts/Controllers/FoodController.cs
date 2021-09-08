using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public int hp = 1;
    public bool isAttached = false;

    public void Rotate(GameObject slot)
    {
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        this.gameObject.layer = 2;
        this.gameObject.transform.parent = slot.transform;
        this.gameObject.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        this.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0);
        this.gameObject.transform.localPosition = new Vector3(0, 0, -0.1f);
        this.gameObject.tag = "Player";
    }
}
