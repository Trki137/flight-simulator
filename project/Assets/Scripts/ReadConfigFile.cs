using UnityEngine;

public class ReadConfigFile : MonoBehaviour
{

    [SerializeField]
    private TextAsset textConfigFile;

    [SerializeField]
    private string[] lines;

    private int numOfNavPoints;

    private int numOfControllablePlains;

    // Start is called before the first frame update
    void Start()
    {
        ReadTextConfig();
        numOfControllablePlains = parseNumOfControllablePlains();
        numOfNavPoints = parseNumOfNavPoints();
    }

    void ReadTextConfig() 
    {
        lines = textConfigFile.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
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

    private int parseNumOfControllablePlains() {

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("controllableAirplains"))
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

    public int getNumOfControllablePlains() 
    {
        return numOfControllablePlains;
    }


    public string[] getLines()
    {
        return lines;
    }
}