using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public FoodController food;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.gameObject.tag == "Player";
        
        if (isPlayer)
        {
            player.GetComponent<PlayerState>().pickupFood(food);
            this.gameObject.layer = 2;
        }
    }
}
