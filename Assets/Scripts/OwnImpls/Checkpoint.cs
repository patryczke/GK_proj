using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    public bool check;
    public Checkpoint previousCheckpoint;
    public const int numberOfCheckpoints = 4;
    
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerExit(Collider col) {
        if (col.gameObject.GetComponent<LapHolder>() && previousCheckpoint.check)
        {
            check = true;
        }

        Checkpoint[] checkpoints = FindObjectsOfType(typeof(Checkpoint)) as Checkpoint[];
        col.gameObject.GetComponent<LapHolder>().allChecked = true;
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (!checkpoint.check)
            {
                col.gameObject.GetComponent<LapHolder>().allChecked = false;
            }
        }

        if (col.gameObject.GetComponent<LapHolder>().allChecked)
        {
            col.gameObject.GetComponent<LapHolder>().laps++;
            foreach (Checkpoint checkpoint in checkpoints)
            {
                    checkpoint.check = false;
            }
            checkpoints[2].check = true;
        }
    }
    
}
