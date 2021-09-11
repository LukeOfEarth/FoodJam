using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject playerPrefab;
    private GameObject player;

    private void Awake()
    {
        CreateLevel();
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        }
    }

    void CreateLevel()
    {
        int i = Random.Range(0, levels.Length - 1);
        GameObject level = Instantiate(levels[i], new Vector3(0, 0, 0), levels[i].transform.rotation);
    }
}
