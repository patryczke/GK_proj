using UnityEngine;
using System.Collections;

public class ResetCar : MonoBehaviour {
    public float resetHeight = 0.5f;
    bool wasGrounded = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R))
        {
            if (wasGrounded)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y + resetHeight, transform.position.z);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                wasGrounded = false;
            }
        }
        if(wasGrounded == false)
        {
            if(GetComponent<CarController>().isGrounded())
            {
                wasGrounded = true;
            }
        }
	}
}
