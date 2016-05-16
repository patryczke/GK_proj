using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {
    RaycastHit hit;
    Ray ray;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        ray = new Ray(transform.position, transform.forward);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.forward * 2, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f) || 
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1,transform.position.z) , transform.forward, out hit, 2f))
        {
           
            if(hit.collider.gameObject.GetComponent<AIMain>() != null || hit.collider.gameObject.GetComponent<CarControllerWheelCollider>() != null)
            {
                Debug.Log("Hit car");
                hit.rigidbody.AddForce(hit.transform.up*20000, ForceMode.Impulse);
                Instantiate(explosion, hit.transform.position,transform.rotation);
            }
            else
            {
                Debug.Log(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.transform != this.transform)
            {
                Destroy(gameObject);
            }
        }

	}
}
