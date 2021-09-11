using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject playerPrefab;
    private GameObject player;
    private GameObject spawnPoint;
    private GameObject sceneManager;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
        CreateLevel();
        spawnPoint = GameObject.Find("SpawnPlayer");
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
            player.transform.position = spawnPoint.transform.position;
        }
    }

    void CreateLevel()
    {
        int i = Random.Range(0, levels.Length - 1);
        GameObject level = Instantiate(levels[i], new Vector3(0, 0, 0), levels[i].transform.rotation);
    }

    public void EndLevel()
    {
        sceneManager.GetComponent<Scenes>().ToGame();
    }
}
