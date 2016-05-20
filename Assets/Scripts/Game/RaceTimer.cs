using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;
using System.Threading;

public class RaceTimer : MonoBehaviour
{

    long startTime;
    long endTime;
    long timeElapsed;
    string timeElapsedFormatted;

    Stopwatch stopwatch;

    public Text timerText;

    void Start()
    {
        timerText = gameObject.GetComponent<Text>();
        stopwatch = new Stopwatch();

        //TimeStart();    //it will be called from outside, when race starts
    }

    void Update()
    {
        timeElapsed = stopwatch.ElapsedMilliseconds;
        long _timeElapsed = timeElapsed;
        long minutes = _timeElapsed / (60 * 1000);
        _timeElapsed -= minutes * 60 * 1000;
        long seconds = _timeElapsed / 1000;
        _timeElapsed -= seconds * 1000;
        long milis = _timeElapsed;
        timerText.text = String.Format("{0:00}:{1:00}:{2:000}" ,minutes ,seconds, milis);
        

    }

    public void TimeStart()
    {
        stopwatch.Start();
    }

    public void TimeStop()
    {
        stopwatch.Stop();
    }

    public long GetTimeElapsed()
    {
        return timeElapsed;
    }
}
