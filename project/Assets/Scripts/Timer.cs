using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeRemaining = 5.0f;
    public static int timesWritten = 0;
    public static string dateTime;
    private bool dateWritten = false;

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0 && !dateWritten){
            dateTime = System.DateTime.Now.ToString("yyyy.dd.MM-HH:mm:ss:fff");
            dateWritten = true;
        }
    }

    public float getRemaining() {
        return timeRemaining;
    }
    public void setTimeRemaining(float timeRemaining) {
        this.timeRemaining = timeRemaining;
        dateWritten = false;
    }
}
