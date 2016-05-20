using UnityEngine;
using System.Collections;
using System;

public class LapHolder : MonoBehaviour {
    public float laps = 0;
    public int maxLaps = 4;
    Checkpoint[] chekpoints;
    public bool[] checkedChekpoints;
    public float distanceToNextChekpoint;
    // Use this for initialization
    void Start () {
        chekpoints = FindObjectsOfType<Checkpoint>();
        Array.Resize(ref checkedChekpoints, chekpoints.Length);
        for (int i = 0; i < chekpoints.Length; i++)
        {
            checkedChekpoints.SetValue(false, i);
        }
        checkedChekpoints.SetValue(true, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //distanceToNextChekpoint = countDistanceToNextChekpoint();
	}

    public void resetChekpoints()
    {
        for(int i = 0; i < checkedChekpoints.Length; i++)
        {
            checkedChekpoints[i] = false;
        }
        checkedChekpoints[0] = true;
    }

    public float countDistanceToNextChekpoint()
    {
        for(int i = 0; i < checkedChekpoints.Length; i++)
        {
            if (checkedChekpoints[i] == false && i != (checkedChekpoints.Length - 1))
            {
                return Vector3.Distance(transform.position, chekpoints[i+1].transform.position);
            }
        }
        return Vector3.Distance(transform.position, chekpoints[checkedChekpoints.Length-1].transform.position);
    }

    public float PlaceHelper()
    {
        float returnable = 0;
        foreach (bool c in checkedChekpoints)
        {
            if(c == true)
            {
                returnable += 2000;
            }
        }
        return laps * 100000 + returnable - countDistanceToNextChekpoint();
    }
}
