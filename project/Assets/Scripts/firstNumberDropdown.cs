using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firstNumberDropdown : MonoBehaviour
{
    Dropdown dropdown;
    public static int dropdownIndex;
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate
        {
            getDropdownValue();
        });
    }

    public void getDropdownValue()
    {
        dropdownIndex = dropdown.value;
    }
}
