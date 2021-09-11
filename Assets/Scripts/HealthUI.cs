using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PlayerState player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = player.hp.ToString();
    }
}
