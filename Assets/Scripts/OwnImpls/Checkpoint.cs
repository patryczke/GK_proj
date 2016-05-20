using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {
    public bool check;
    public Checkpoint previousCheckpoint;
    public const int numberOfCheckpoints = 4;
    public int checkpointNumber;
    DataManager data;
    // Use this for initialization
    void Start () {
        data = FindObjectOfType<DataManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerExit(Collider col) {
        if (col.gameObject.GetComponent<LapHolder>())
        {
            if (checkpointNumber > 0 && col.gameObject.GetComponent<LapHolder>().checkedChekpoints[checkpointNumber - 1])
            {
                col.gameObject.GetComponent<LapHolder>().checkedChekpoints[checkpointNumber] = true;
                //FindObjectOfType<CurrentPositionText>().deleteObjectAndInsertAtFirst(col.gameObject.GetComponent<LapHolder>());
            }
            if(checkpointNumber == col.gameObject.GetComponent<LapHolder>().checkedChekpoints.Length - 1 && col.gameObject.GetComponent<LapHolder>().checkedChekpoints[checkpointNumber - 1])
            {
                if (col.gameObject.GetComponent<LapHolder>().laps + 1 >= col.gameObject.GetComponent<LapHolder>().maxLaps)
                {
                    if (col.gameObject.GetComponent<CarControllerWheelCollider>())
                    {
                        col.gameObject.GetComponent<CarControllerWheelCollider>().SwitchOff();
                    }
                    else
                    {
                        if (col.gameObject.GetComponent<AIMain>())
                        {
                            col.gameObject.GetComponent<AIMain>().waypointContainer = null;
                            col.gameObject.GetComponent<AIMain>().SetWaypoints(null);
                            col.gameObject.GetComponent<AIMain>().accelerationPower = 0;
                        }
                    }
                    FindObjectOfType<RaceTimer>().TimeStop();
                    if (data)
                    {
                        data.recordTime = FindObjectOfType<RaceTimer>().timerText.text;
                        StartCoroutine(LoadMenu());
                    }
                }
                else
                {
                    col.gameObject.GetComponent<LapHolder>().laps++;
                }
                col.gameObject.GetComponent<LapHolder>().resetChekpoints();
            }
        }
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
