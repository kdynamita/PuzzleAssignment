using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Stats stats;
    [Space]
    public Player spawnablePrefab; // what prefab that can be spawned
    public int totalSpawned = 0; // total number of prefabs spawned
    public int totalMoves = 0;
    public int totalScore = 0;

    [SerializeField] private List<Player> spawnedList; // list that holds what is spawned

    private Scene currentScene;
    private int sceneIndex;

    #region // - - - - - - NOTE TO RAMY - - - - -  NOTE TO RAMY - - - - - NOTE TO RAMY - - - - -
    // I've made these two variables for a terrible check for player on the scene
    // Made sure it only triggered if the player is not on the scene
    // REFER TO NOTE ON LINE 41!
    public GameObject playerOnScene;
    public bool onScene = true;
    #endregion


    // remake the list of spawned objects upon start
    void Start()
    {
        spawnedList = new List<Player>();
        if (spawnablePrefab == null)
            spawnablePrefab = Resources.Load<GameObject>("Player").GetComponent<Player>();
    }

    // Instantiating the object that will be spawned 
    // with the Player script in it
    // setting the spawned object's manager as THIS script
    // setting position of spawned object
    // adds to the total number of spawned objects

    void Update()
    {
        #region // - - - - - - NOTE TO RAMY - - - - -  NOTE TO RAMY - - - - - NOTE TO RAMY - - - - -
        // Not pretty, but I couldn't get any scripts to work upon collision with the killing paddle, before the player gets destroyed
        // I know there's ways, but I've been spending TOO MUCH TIME TRYING TO FIGURE IT OUT !!!!!
        // So here's my awful player check :D

        #endregion
        CheckSpawn();
    }

    public void UpdateStats()
    {
        
    }

    public void CheckSpawn()
    {
        playerOnScene = GameObject.Find("Player");
        if (playerOnScene == null)
            Spawn();
        else
            return;
    }

    public GameObject Spawn()
    {
        if (!onScene && playerOnScene == null)
        {
            Debug.Log("RESPAWNING");
            onScene = true;
            Player spawned = Instantiate<Player>(this.spawnablePrefab);
            spawnedList.Add(spawned);
            spawned.manager = this;
            spawned.transform.position = new Vector3(0, 2, -8);
            totalSpawned++;
            Debug.Log("RESPAWNED");

            return spawned.gameObject;
        }

        else
        {
            return null;
        }
    }

    // Delete all prefabs spawned
    // for every spawned object part of the spawnedList
    // destroy object
    // clear the spawnedList entirely
    public void DeleteAll()
    {
        foreach (Player spawn in this.spawnedList)
        {
            Destroy(spawn.gameObject);
        }
        this.spawnedList.Clear();
    }


    // If the scene number is lower than the total quantity of scenes available, go to the next scene level
    // when reaching the maximum amount of scenes, go to the win scene
    public void Won()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneIndex = currentScene.buildIndex;

        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Level1");
                break;
            case 1:
                SceneManager.LoadScene("Level2");
                break;
            case 2:
                SceneManager.LoadScene("Level3");
                break;

            case 3:
                SceneManager.LoadScene("Level4");
                break;

            case 4:
                SceneManager.LoadScene("Win");
                break;
        }
    }
}
