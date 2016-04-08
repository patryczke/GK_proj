using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    public Vector3 movementMultiplier;
    public bool forward;
    public bool right;
    public bool up;
    Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (forward)
            rigidbody.MovePosition(transform.position + transform.forward * movementMultiplier.z * Time.deltaTime);
            //transform.position += transform.forward * movementMultiplier.z;
        if(up)
            transform.position += transform.up * movementMultiplier.y;
        if (right)
            transform.position += transform.right * movementMultiplier.x;
    }
}
