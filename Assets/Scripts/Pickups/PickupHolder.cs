using UnityEngine;
using System.Collections;

public class PickupHolder : MonoBehaviour {
    PickupManager pickupManager;
    public GameObject activePickup;
    public GameObject[] pickupList;
    int pickupIndex;
    // Use this for initialization
    void Start () {
        pickupManager = FindObjectOfType<PickupManager>();
        if(pickupManager)
        SetPickupList(pickupManager.pickupList);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomPickup()
    {
        pickupIndex = Random.Range(0, pickupList.Length);
        activePickup = pickupList[pickupIndex];
    }

    public void SetPickupList(GameObject[] pickupListTemp)
    {
        pickupList = pickupListTemp;
    }

    public void SpawnPickup()
    {
        if (activePickup.GetComponent<OnCarWeapon>() != null)
        {
            activePickup.GetComponent<OnCarWeapon>().parent = gameObject;
            Instantiate(activePickup, transform.position + transform.forward + transform.up, transform.rotation);
        }
        else {
            Instantiate(activePickup, transform.position + transform.forward * 10 + transform.up, transform.rotation);
        }
    }
}
