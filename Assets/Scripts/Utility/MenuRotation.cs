using UnityEngine;
using System.Collections;

public class MenuRotation : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 actualRotation = transform.eulerAngles;
        actualRotation.y += speed;
        transform.eulerAngles = actualRotation;
	}
}
