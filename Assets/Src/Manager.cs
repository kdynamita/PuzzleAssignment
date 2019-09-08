using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Player spawnablePrefab; // what prefab that can be spawned
    public int totalSpawned = 0; // total number of prefabs spawned

    [SerializeField]private List<Player> spawnedList; // list that holds what is spawned
    [SerializeField]private int sceneNum = 2;
    [SerializeField]private int sceneLimit = 5;


    // remake the list of spawned objects upon start
    void Start()
    {
        spawnedList = new List<Player>();
    }


    // Instantiating the object that will be spawned 
    // with the Managed script in it
    // setting the spawned object's manager as THIS script
    // setting position of spawned object
    // adds to the total number of spawned objects
    public GameObject Spawn()
    {
        spawnablePrefab = Resources.Load<GameObject>("Player").GetComponent<Player>();
        Player spawned = Instantiate<Player>(this.spawnablePrefab);
        spawnedList.Add(spawned);
        spawned.manager = this;
        spawned.transform.position = new Vector3(Random.value * 10, this.transform.position.y, Random.value * 10);
        totalSpawned++;
        return spawned.gameObject;
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

    public void Won()
    {
        if (sceneNum < sceneLimit)
             SceneManager.LoadScene("Level" + sceneNum++);
        else {
            SceneManager.LoadScene("Win");
        }
    }
}
