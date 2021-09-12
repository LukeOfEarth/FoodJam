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
    public GameObject gameMusic;

    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("GameMusic") == null)
        {
            Instantiate(gameMusic);
        }

        if(GameObject.Find("MusicManager") != null)
        {
            Destroy(GameObject.Find("MusicManager"));
        }

        sceneManager = GameObject.Find("SceneManager");
        CreateLevel();
        spawnPoint = GameObject.Find("SpawnPlayer");
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        }

        player.transform.position = spawnPoint.transform.position;
        player.GetComponentInChildren<GrapplingGun>().CRW();
    }

    void CreateLevel()
    {
        int i = Random.Range(0, levels.Length);
        GameObject level = Instantiate(levels[i], new Vector3(0, 0, 0), levels[i].transform.rotation);
    }

    public void EndLevel()
    {
        sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<Scenes>().ToGame();
    }
}
