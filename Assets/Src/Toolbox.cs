using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    // Make this script STATIC named as _instance in order to keep it centralized
    // Remain private so that nothing else can edit this script
	[SerializeField]private static Toolbox _instance;

    // GetInstance() is the interactable static variable
    // that will check if the script is present in the scene
    // if not, then create a new "Toolbox" gameObject
    // make sure Toolbox persists by making it undestroyable on load
    // add this script to the Toolbox gameObject
	public static Toolbox GetInstance() {
		if (Toolbox._instance == null) {
			var go = new GameObject("Toolbox");
			DontDestroyOnLoad(go);
			Toolbox._instance = go.AddComponent<Toolbox>();
		}
		return Toolbox._instance;
	}

	private Manager manager;


    // Before the game starts, check if a Toolbox instance already exists
    // If it does, destroy this one && keep the already existing one 
    // Create a new gameObject called "Manager"
    // make the Toolbox gameObject the parent of the Manager
    // Add the "Manager" script to the gameObject
	void Awake()
    {
        
        if (Toolbox._instance != null) {
			Destroy(this);
		}

		var go = new GameObject("Manager");
		go.transform.parent = this.gameObject.transform;
		this.manager = go.AddComponent<Manager>();
		//this.manager.spawnablePrefab = Resources.Load<GameObject>("SpawnablePrefab").GetComponent<Player>();
	}

    // Public function that will allow other objects to pick the same Manager as the Toolbox's
	public Manager GetManager() {
		return this.manager;
	}
}
