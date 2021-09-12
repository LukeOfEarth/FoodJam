using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public FoodController food;
    private GameObject player;
    public GameObject soundFx;
    public AudioClip pickup;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.gameObject.tag == "Player";
        
        if (isPlayer)
        {
            player.GetComponent<PlayerState>().pickupFood(food);
            GameObject sound = Instantiate(soundFx);
            sound.GetComponent<SoundFX>().PlaySound(pickup);
        }
    }
}
