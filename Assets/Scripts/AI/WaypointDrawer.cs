using UnityEngine;
using System.Collections;

public class WaypointDrawer : MonoBehaviour {
    Transform[] next;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        next = GetComponentsInChildren<Transform>();
        for (int i = 0; i < next.Length; i++)
        { 
            Gizmos.color = Color.green;
            if (next[i].position != transform.position)
            {
                Gizmos.DrawWireSphere(next[i].position, 1);
                Gizmos.DrawLine(next[i-1].position, next[i].position);
            }
        }
    }
}
