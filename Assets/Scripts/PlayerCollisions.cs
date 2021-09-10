using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    PlayerState playerState;
    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            print("test");
            int damage = 1;
            playerState.TakeDamage(damage);
        }
    }
}
