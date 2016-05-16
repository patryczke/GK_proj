using UnityEngine;
using System.Collections;

public class ShootWeapon : MonoBehaviour {
    PickupHolder pickupHold;
	// Use this for initialization
	void Start () {
        pickupHold = GetComponent<PickupHolder>();
	}
	
	// Update is called once per frame
	void Update () {
        if (pickupHold.activePickup && Input.GetAxis("Shoot") != 0)
        {
            pickupHold.SpawnPickup();
            pickupHold.activePickup = null;
        }
	}
}
