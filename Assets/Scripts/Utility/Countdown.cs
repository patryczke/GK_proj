using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    float timer = 3f;
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        GetComponent<Text>().text = ((int)(timer+1f)).ToString();
        if(timer <= 0f)
        {
            GetComponent<Text>().text = "";
            FindObjectOfType<RaceTimer>().TimeStart();
        }
	}
}
