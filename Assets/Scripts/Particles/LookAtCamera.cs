using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
    Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
            transform.forward *= -1;
        }
    }
}
