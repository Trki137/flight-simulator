
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navPoints : MonoBehaviour
{
    [SerializeField]
    private WriteData writeLog;

    public int brojNavTocaka;
    
    public GameObject prefab;

    private string dateTime = System.DateTime.Now.ToString("yyyy.dd.MM-HH:mm:ss:fff");

    private Vector2 pozicija;

    private List<Vector2> positionOfAllNavPoints;
    // Start is called before the first frame update
    void Start()
    {
            positionOfAllNavPoints = new List<Vector2>();
            stvoriNavTocke();

            string log = string.Format("[{0}] Screen height is {1} pixels\n[{2}] Screen width is {3} pixels", dateTime,Screen.height,dateTime,Screen.width);
            writeLog.writeLog(log);

    }

    // Update is called once per frame
    public void stvoriNavTocke()
    {
           for(int i = 0; i < brojNavTocaka; i++) {
             float X;
             float Y;
              Random.InitState (Random.Range(Random.Range(Random.Range(Random.Range(0, 25), Random.Range(324, 5673)), Random.Range(Random.Range(53, 2378), Random.Range(50, 423))), Random.Range(Random.Range(Random.Range(23, 2354), Random.Range(1, 3456)), Random.Range(Random.Range(7, 32421), Random.Range(8, 23472)))));
                if(i%4==0){
                X = Random.Range(0f, 8f);
                Y = Random.Range(0f, 3.5f);
                } else if(i%4==1) {
                X = Random.Range(0f, 8f);
                Y = Random.Range(0f, -3.5f);
                }else if(i%4==2) {
                X = Random.Range(0f, -8f);
                Y = Random.Range(0f, -3.5f);
                }else if(i%4==3) {
                X = Random.Range(0f, -8f);
                Y = Random.Range(0f, 3.5f);
                } else {
                X = Random.Range(-5f, 5f);
                Y = Random.Range(-3f, 3f); 
                }


            pozicija = new Vector2(X,Y);
            GameObject tocka = Instantiate(prefab, pozicija, prefab.transform.rotation);
           /*RANDOM BOJA -> tocka.GetComponent <Renderer> ().material.color = new Color (Random.Range (0F, 1F), Random.Range (0F, 1F), Random.Range (0F, 1F), 1);*/
            tocka.GetComponent <Renderer> ().material.color = new Color (255, 165, 0);
           // tocka.GetComponent <TMPro.TextMeshProUGUI> ().text +=i;
            GameObject T = tocka.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            tekst.text+=i.ToString();

            string log = string.Format("[{0}] Navigation point {1} is at position ({2},{3})", dateTime ,i + 1, pozicija.x, pozicija.y);
            writeLog.writeLog(log);

            positionOfAllNavPoints.Add(pozicija);

         }  
    }

    public List<Vector2> getAllNavPositions() {
        return positionOfAllNavPoints;
    }

}