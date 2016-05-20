using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurrentPositionText : MonoBehaviour {
    LapHolder[] cars;
    LapHolder playersCar;
    public List<LapHolder> sortedCars;
    float[] helper = new float[6];
    int playersPlace = 5;
	// Use this for initialization
	void Start () {
	    cars = FindObjectsOfType<LapHolder>();
        playersCar = FindObjectOfType<CarControllerWheelCollider>().gameObject.GetComponent<LapHolder>();
        sortedCars.AddRange(cars);
	}
	
	
	void FixedUpdate () {
        for(int i = 0; i < cars.Length; i++)
        {
            helper[i] = cars[i].PlaceHelper();
        }
        for(int i = 0; i < helper.Length - 1; i++)
        {
            playersPlace = 0;
            if(helper[5] <= helper[i])
            {
                playersPlace++;
            }
        }
        GetComponent<Text>().text = (1 + playersPlace).ToString();
	}

    private bool ContainsPlayer(LapHolder val)
    {
        return val == playersCar;
    }

    public void deleteObjectAndInsertAtFirst(LapHolder car)
    {
        sortedCars.Remove(car);
        sortedCars.Insert(0, car);
    }
}
