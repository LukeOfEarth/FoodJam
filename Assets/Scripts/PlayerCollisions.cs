using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    PlayerState playerState;
    LevelManager levelManager;
    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spikes")
        {
            int damage = 1;
            playerState.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            int damage = 1;
            playerState.TakeDamage(damage);
        }
        else if (collision.gameObject.tag == "Endpoint")
        {
            levelManager.EndLevel();
        }
    }
}
