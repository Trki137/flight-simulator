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

    void Start()
    {

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

        num = (firstNumberDropdown.dropdownIndex-1) * 100 + (secondNumberDropdown.dropdownIndex) *10;

       // turn = dropdownIndex == 1 ? "Left" : "Right";
        if(dropdownIndex == 1) turn = "Left";
        else if(dropdownIndex==2) turn = "Right";
        
    if(dropdownIndex>0) {
        Debug.Log("Turn for "+ num + "deg to "+ turn);}
     }
}
