using UnityEngine;
using System.Collections.Generic;

public class ReadConfigFile : MonoBehaviour
{

    [SerializeField]
    private TextAsset textConfigFile;

    [SerializeField]
    private string[] lines;

    private int numOfNavPoints;

    private int numOfControllablePlanes;

    private int numOfUncontrollablePlanes;

    private List<int[]> navigationOrder;

    // Start is called before the first frame update
    void Start()
    {
        ReadTextConfig();
        numOfControllablePlanes = parseNumOfControllablePlanes();
        numOfUncontrollablePlanes = parseNumOfUncontrollablePlanes();
        numOfNavPoints = parseNumOfNavPoints();
        navigationOrder = getNavigationOrder();
    }

    void ReadTextConfig() 
    {
        lines = textConfigFile.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
    }

    private List<int[]> getNavigationOrder() {
        List<int[]> navigationOrder = new List<int[]>();

        if (numOfControllablePlanes != lines.Length - 3)
            throw new System.Exception("Configuration file in invalid format. Every airplain has to have navigation point order");

        for (int start = 3; start < lines.Length; start++) {
            string[] navigationNumbers = lines[start].Split(new string[] { "," }, System.StringSplitOptions.None);

            int[] order = new int[numOfNavPoints];

            if (navigationNumbers.Length != numOfNavPoints)
                throw new System.Exception("Configuration file is not in correct format. Check navigation point");

            for (int i = 0; i < navigationNumbers.Length; i++) {
                order[i] = int.Parse(navigationNumbers[i]);
            }

            navigationOrder.Add(order);
        }

        return navigationOrder;
    }


    private int parseNumOfNavPoints() {

        for (int i = 0; i < lines.Length; i++) 
        {
            if (lines[i].Contains("navPoints")) 
            {
                return parse(lines[i]);
            }
        }
        return 0;
    }

    private int parseNumOfControllablePlanes() {

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("controllableAirplanes"))
            {
                return parse(lines[i]);
            }
        }

        return 0;
    }

    private int parseNumOfUncontrollablePlanes()
    {

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("redAirplanes"))
            {
                return parse(lines[i]);
            }
        }

        return 0;
    }

    private int parse(string value) 
    { 
        return int.Parse(value.Substring(value.IndexOf("=") + 1));
    }

    public int getNumOfNavPoints() 
    {
        return numOfNavPoints;
    }

    public int getNumOfControllablePlanes() 
    {
        return numOfControllablePlanes;
    }


    public string[] getLines()
    {
        return lines;
    }

    public int getNumOfUncontrollablePlanes() {
        return numOfUncontrollablePlanes;
    }

    public int[] getOrderForIndex(int index) {
        return navigationOrder[index];
    }
}
