using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public float rotationSpeed = 10;
    public CarController car;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (car.isGrounded())
        {
            transform.Rotate(Vector3.forward, -car.actualSpeed * Time.deltaTime * rotationSpeed);
        }
    }
}
