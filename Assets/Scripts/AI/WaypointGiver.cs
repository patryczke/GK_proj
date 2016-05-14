using UnityEngine;
using System.Collections;

public class WaypointGiver : MonoBehaviour {
    AIMain[] AICars;
    Transform[] waypoints;
	// Use this for initialization
    void Awake()
    {

       
    }
	void Start () {
        waypoints = GetComponentsInChildren<Transform>();
        StartCoroutine(Delay());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        AICars = FindObjectsOfType<AIMain>();
        foreach (AIMain car in AICars)
        {
            car.SetWaypoints(waypoints);
            car.waypointContainer = transform;
        }
        CarControllerWheelCollider player = FindObjectOfType<CarControllerWheelCollider>();

        player.SwitchOn();
    }
}
