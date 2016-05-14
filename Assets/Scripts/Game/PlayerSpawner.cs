using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    DataManager data;
	// Use this for initialization
	void Awake () {
        data = FindObjectOfType<DataManager>();
        if(data != null)
        {
            Instantiate(data.cars[data.chosenCar], transform.position, transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
