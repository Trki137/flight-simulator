using UnityEngine;
using System.Collections;


// An FPS counter.
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
public class TimerScript : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;

    private System.DateTime TimeStart;
    private System.DateTime TimeEnd;

    private WriteData myObject;

    private ColiderCounter coliderCounter;

    void Start()
    {

        GameObject G = GameObject.Find("WriteData");
        myObject = G.GetComponent<WriteData>();
        
        G = GameObject.Find("ColliderCounter");
        coliderCounter = G.GetComponent<ColiderCounter>();

        TimeStart = System.DateTime.Now;
        System.TimeSpan time = new System.TimeSpan(0, 0, 3, 0);
        TimeEnd = TimeStart.Add(time);
        Debug.Log(TimeStart);
        Debug.Log(TimeEnd);
    }

    void Update()
    {
        System.DateTime TimeCurrent = System.DateTime.Now;
        if(TimeCurrent.ToString("HH:mm:ss") == TimeEnd.ToString("HH:mm:ss")) {
            Application.Quit();
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.mm-HH:mm:ss:fff");
            string log = string.Format("[{0}] Simulation finished, total number of collides is {1} ", dateTime, coliderCounter.getCounter());
            myObject.writeLog(log);
            UnityEditor.EditorApplication.isPlaying = false;
        } 
    }
}