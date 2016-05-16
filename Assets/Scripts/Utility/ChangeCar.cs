using UnityEngine;
using System.Collections;

public class ChangeCar : MonoBehaviour {
    public CarController[] cars;
    public CameraFollow camera;
    int actualCar = 0;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.C))
        {
            cars[actualCar].gameObject.SetActive(false);
            if(++actualCar >= cars.Length)
            {
                actualCar = 0;
            }
            cars[actualCar].gameObject.SetActive(true);
            camera.target = cars[actualCar].cameraPoint.transform;
        }
	}
}
