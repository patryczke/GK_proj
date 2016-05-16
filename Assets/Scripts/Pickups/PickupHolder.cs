using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickupHolder : MonoBehaviour {
    PickupManager pickupManager;
    public GameObject activePickup;
    public GameObject[] pickupList;
    int pickupIndex;
    public PickupText pickupText;
    // Use this for initialization
    void Start () {
        pickupManager = FindObjectOfType<PickupManager>();
        if(pickupManager)
        SetPickupList(pickupManager.pickupList);
        pickupText = Resources.FindObjectsOfTypeAll<PickupText>()[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomPickup()
    {
        pickupIndex = Random.Range(0, pickupList.Length);
        activePickup = pickupList[pickupIndex];
        pickupText.gameObject.SetActive(true);
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
            pickupText.gameObject.SetActive(false);
        }
        else {
            Instantiate(activePickup, transform.position + transform.forward * 10 + transform.up, transform.rotation);
            pickupText.gameObject.SetActive(false);
        }
        
    }
}
