using UnityEngine;
using System.Collections;

[System.Serializable]
public enum GroundTypes
{
    Road,
    Grass,
    TiledFloor
};

public class GroundType : MonoBehaviour {
    public GroundTypes type;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
