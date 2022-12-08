using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secondNumberDropdown : MonoBehaviour
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
        dropdownIndex = this.dropdown.value;
    }
}
