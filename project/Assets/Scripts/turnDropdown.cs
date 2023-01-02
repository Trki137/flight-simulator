using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnDropdown : MonoBehaviour
{
    Dropdown dropdown;
    private int dropdownIndex;
    public static int num=0;
    public static string turn;

    [SerializeField]
    private WriteData writeLog;

    void Start()
    {

        GameObject G = GameObject.Find("WriteData");
        writeLog = G.GetComponent<WriteData>();

        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate
        {
            setNewCourse();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setNewCourse()
    {



        dropdownIndex = this.dropdown.value;

       if(firstNumberDropdown.dropdownIndex-1 == 0) {
             num = (firstNumberDropdown.dropdownIndex-1) * 100 + (secondNumberDropdown.dropdownIndex) *10;
        } else {
             num = (firstNumberDropdown.dropdownIndex-1)* 100 + (secondNumberDropdown.dropdownIndex-1) *10;
        }

       // turn = dropdownIndex == 1 ? "Left" : "Right";
        if(dropdownIndex == 1) turn = "Left";
        else if(dropdownIndex==2) turn = "Right";
        
    if(dropdownIndex>0) {
        string dateTime = System.DateTime.Now.ToString("yyyy.dd.mm-HH:mm:ss:fff");
        string log = string.Format("[{0}] Turn for {1} deg  to {2}", dateTime, num, turn);
        writeLog.writeLog(log);
        
     }
    }
}
