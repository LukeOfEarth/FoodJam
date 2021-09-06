using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector2 dir, float thrust)
    {
        if(!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.AddForce((dir.normalized) * thrust, ForceMode2D.Force);
    }
}
