﻿using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public float rotationSpeed = 10;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
            transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
    }
}
