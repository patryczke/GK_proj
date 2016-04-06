using UnityEngine;
using System.Collections;

public class WaypointGiver : MonoBehaviour {
    AIMain[] AICars;
    Transform[] waypoints;
	// Use this for initialization
    void Awake()
    {
        AICars = FindObjectsOfType<AIMain>();
    }
	void Start () {
        waypoints = GetComponentsInChildren<Transform>();
        foreach(AIMain car in AICars)
        {
            car.SetWaypoints(waypoints);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
