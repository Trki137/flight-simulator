using UnityEngine;
using System.IO;

public class WriteData : MonoBehaviour
{
    private TextWriter tw;

    private const string PATH = "Assets/Config/data.txt";
    void Start()
    {
        if (File.Exists(PATH))
        {
            File.Delete(PATH);
        }
        
    }

    public void writeLog(string log) 
    {
        tw = new StreamWriter(PATH, append: true);
        tw.WriteLine(log);
        tw.Close();
    }

}