using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedCounter : MonoBehaviour {
    Text speedText;
    float speed;
    CarController car;
    CameraFollow mainCamera;

	// Use this for initialization
	void Start () {
        speedText = GetComponent<Text>();
        mainCamera = FindObjectOfType<CameraFollow>();
        if (mainCamera.target.transform.parent.GetComponent<CarController>())
        {
            car = mainCamera.target.transform.parent.GetComponent<CarController>();
            speed = car.actualSpeed;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (mainCamera.target.transform.parent.GetComponent<CarController>())
        {
            car = mainCamera.target.transform.parent.GetComponent<CarController>();
            speed = car.actualSpeed;
        }
        speedText.text = ((int)speed).ToString();
    }
}
