using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    public GameObject[] cars;
    public string[] tracks;
    public int chosenCar;
    public int chosenTrack;
    public string recordTime;
    public Text recordText;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
        if(recordTime == "")
        {
            recordTime = "0:00.000";
        }
        recordText.text = recordTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCar(int car)
    {
        chosenCar = car;
    }

    public void setTrack(int track)
    {
        chosenTrack = track;
    }

    public void loadTrack()
    {
        SceneManager.LoadScene(tracks[chosenTrack]);
    }
}
