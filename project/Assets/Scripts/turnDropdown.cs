using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnDropdown : MonoBehaviour
{
    Dropdown dropdown;
    private int dropdownIndex;
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

        int num = firstNumberDropdown.dropdownIndex * 100 + (secondNumberDropdown.dropdownIndex + 1) * 10;
        string turn = dropdownIndex == 0 ? "Left" : "Right";

        Debug.Log("Turn for "+ num + "deg to "+ turn);
     }
}
