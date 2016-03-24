using UnityEngine;
using System.Collections;

public class WheelVisual : MonoBehaviour {
    public GameObject[] frontWheels;
    public GameObject[] backWheels;
    public float maxSteerAngle = 15;
    public CarController car;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //front wheels are steerable
            foreach (GameObject wheel in frontWheels)
            {
                wheel.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, car.transform.eulerAngles.y + Input.GetAxis("Horizontal") * maxSteerAngle * car.turnSpeed, car.transform.eulerAngles.z + 90);
            }
        }
        else
        {
            foreach (GameObject wheel in frontWheels)
            {
                wheel.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, car.transform.eulerAngles.y, car.transform.eulerAngles.z + 90);
            }
        }
    }
}
