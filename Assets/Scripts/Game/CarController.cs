﻿using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    Rigidbody playersRigidbody;
    public float accelerationPower = 10;
    public float brakingPower = 10;
    public float maxSpeed = 20;
    public float turnSpeed = 10;
    Vector3 rotation = new Vector3(0,100,0);
    public GameObject groundCheck;
    // Use this for initialization
    void Start () {
        playersRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isGrounded())
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * accelerationPower);
                }
                else
                {
                    playersRigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * brakingPower);
                }
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                //playersRigidbody.AddTorque(transform.up * Input.GetAxis("Horizontal") * turnSpeed);
                Quaternion deltaRotation = Quaternion.Euler(rotation * Input.GetAxis("Horizontal") * turnSpeed / 100);
                playersRigidbody.MoveRotation(playersRigidbody.rotation * deltaRotation);
            }
            if (playersRigidbody.velocity.magnitude >= maxSpeed)
            {
                playersRigidbody.velocity = playersRigidbody.velocity.normalized * maxSpeed;
            }
        }
	}

    bool isGrounded()
    {
        if (!Physics.Linecast(playersRigidbody.position, groundCheck.transform.position))
        {
            return false;
        }
        return true;
    }
}
