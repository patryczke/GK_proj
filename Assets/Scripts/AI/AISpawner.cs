using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour {
    public GameObject AIPrefab;
	// Use this for initialization
	void Awake () {
        Instantiate(AIPrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
