using UnityEngine;
using System.Collections;

public class WheelVisual : MonoBehaviour {
    public GameObject[] frontWheels;
    public GameObject[] backWheels;
    public float maxSteerAngle = 15;
    public CarController car;
    Vector3 wheelTransformAtStart;
	// Use this for initialization
	void Start () {
        wheelTransformAtStart = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //front wheels are steerable
            foreach (GameObject wheel in frontWheels)
            {
                wheel.transform.eulerAngles = new Vector3(car.transform.eulerAngles.z + transform.rotation.eulerAngles.x - car.transform.eulerAngles.x, 90 + wheelTransformAtStart.y + car.transform.eulerAngles.y + Input.GetAxis("Horizontal") * maxSteerAngle * car.turnSpeed,0);
            }
        }
        else
        {
            foreach (GameObject wheel in frontWheels)
            {
                wheel.transform.eulerAngles = new Vector3(car.transform.eulerAngles.z + transform.rotation.eulerAngles.x - car.transform.eulerAngles.x, wheelTransformAtStart.y + car.transform.eulerAngles.y + 90, 0);
            }
        }
    }
}
