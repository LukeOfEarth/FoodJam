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
    public void Scatter()
    {
        this.gameObject.transform.parent = null;
        PolygonCollider2D col = this.gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = false;
        int rand = Random.Range(0, 10);
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        Vector3 direction = new Vector3(0f, 0.7f, 0);
        if(rand < 5)
        {
            direction.x = -0.7f;
        }
        else
        {
            direction.x = 0.7f;
        }
        rb.AddForce(500 * direction);
        rb.gravityScale = 2;
        Animation anim = this.gameObject.AddComponent<Animation>();
        anim.rotateZ = true;
        anim.rotateZSpeed = 15;
        StartCoroutine("Kill");
    }
    IEnumerator Kill()
    {
        StartCoroutine("Flash");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.01f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        StartCoroutine("Flash");
    }
}
