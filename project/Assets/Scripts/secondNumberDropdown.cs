using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class secondNumberDropdown : MonoBehaviour
{
   
    public static int dropdownIndex;
     Dropdown dropdown;
    void Start()
    {
         dropdown = GetComponent<Dropdown>();
          dropdown.onValueChanged.AddListener(delegate
        {
            getDropdownValue(dropdown);
        });
        
        dropdownIndex = this.dropdown.value;
            dropdown.options.Clear();
            List<string> items = new List<string>();
            items.Add("-");
            items.Add("0");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");
            items.Add("7");
            items.Add("8");
            items.Add("9");

            foreach(var item in items)
             {
                dropdown.options.Add(new Dropdown.OptionData() {text = item + "0"});
             }
    }

     void Update()
    {
dropdown = GetComponent<Dropdown>();
          dropdown.onValueChanged.AddListener(delegate
        {
            getDropdownValue(dropdown);
        });
        
        dropdownIndex = this.dropdown.value;
         
        if(firstNumberDropdown.dropdownIndex-1 == 0) {
          
            dropdown.options.Clear();
            List<string> items = new List<string>();
            items.Add("-");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");
            items.Add("7");
            items.Add("8");
            items.Add("9");

            foreach(var item in items)
             {
                if(item != "-")
                dropdown.options.Add(new Dropdown.OptionData() {text = item + "0"});
             }
        } else if(firstNumberDropdown.dropdownIndex-1 == 1 || firstNumberDropdown.dropdownIndex-1 == 2) {
             dropdown.options.Clear();
            List<string> items = new List<string>();
            items.Add("-");
            items.Add("0");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");
            items.Add("7");
            items.Add("8");
            items.Add("9");

            foreach(var item in items)
             {
                if(item == "-") {
                     dropdown.options.Add(new Dropdown.OptionData() {text = item});
                } else {
                    dropdown.options.Add(new Dropdown.OptionData() {text = item + "0"});
             }
                }
                
        } else if(firstNumberDropdown.dropdownIndex-1 == 3) {
            dropdown.options.Clear();
            List<string> items = new List<string>();
            items.Add("-");
            items.Add("0");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");

            foreach(var item in items)
             {
                 if(item == "-") {
                     dropdown.options.Add(new Dropdown.OptionData() {text = item});
                } else {
                    dropdown.options.Add(new Dropdown.OptionData() {text = item + "0"});
                }
             }
        } else {
 dropdown.options.Clear();
            List<string> items = new List<string>();
            items.Add("-");
            items.Add("0");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");
            items.Add("7");
            items.Add("8");
            items.Add("9");

            foreach(var item in items)
             {
                 if(item == "-") {
                     dropdown.options.Add(new Dropdown.OptionData() {text = item});
                } else {
                    dropdown.options.Add(new Dropdown.OptionData() {text = item + "0"});
                }
             }
        }
    }

    public void getDropdownValue(Dropdown dropdown)
    {
        dropdownIndex = dropdown.value;
        
      
    }
}
