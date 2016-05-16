using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedCounter : MonoBehaviour {
    Text speedText;
    float speed;
    CarController car;
    CarControllerWheelCollider carW;
    CameraFollow mainCamera;

	// Use this for initialization
	void Start () {
        speedText = GetComponent<Text>();
        mainCamera = FindObjectOfType<CameraFollow>();

            carW = mainCamera.target.transform.parent.GetComponent<CarControllerWheelCollider>();
            speed = carW.actualSpeed;
        

	}
	
	// Update is called once per frame
	void Update () {
        if (carW)
        {
            speed = carW.actualSpeed;
        }
        speedText.text = ((int)speed).ToString();
    }
}
