using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class turnDropdown : MonoBehaviour
{
    Dropdown dropdown;
    private int dropdownIndex;
    public static int num = 0;
    public static string turn;
    public static bool changed = false;

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

        if (firstNumberDropdown.dropdownIndex - 1 == 0)
        {
            num = (firstNumberDropdown.dropdownIndex - 1) * 100 + (secondNumberDropdown.dropdownIndex + 1) * 10;
        }
        else
        {
            num = (firstNumberDropdown.dropdownIndex - 1) * 100 + (secondNumberDropdown.dropdownIndex - 1) * 10;
        }
        Debug.Log(num);

        // turn = dropdownIndex == 1 ? "Left" : "Right";
        if (dropdownIndex == 1)
        {
            turn = "Left";

        }
        else if (dropdownIndex == 2)
        {

            turn = "Right";
        }
        changed = true;

        if (dropdownIndex > 0)
        {

            Debug.Log("Turn for " + num + "deg to " + turn);
        }
    }
}