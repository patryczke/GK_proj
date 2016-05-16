using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour {
    public GameObject AIPrefab;
	// Use this for initialization
	void Start () {
        Instantiate(AIPrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
