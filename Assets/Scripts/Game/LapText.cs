using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LapText : MonoBehaviour {

    public CarControllerWheelCollider playersCar;
	// Use this for initialization
	void Start () {
        StartCoroutine(FindPlayersCar());
	}
	
	// Update is called once per frame
	void Update () {
	    if(playersCar)
        {
            GetComponent<Text>().text = playersCar.gameObject.GetComponent<LapHolder>().laps.ToString();
        }
	}

    IEnumerator FindPlayersCar()
    {
        yield return new WaitForSeconds(1f);
        playersCar = FindObjectOfType<CarControllerWheelCollider>();
    }
}
