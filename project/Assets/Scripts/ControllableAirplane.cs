using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableAirplane : MonoBehaviour
{
    private float speed = 0.005f;

    private Rigidbody2D myBody;

    private Transform[] navigationPoint = new Transform[2];
    private Transform currentTarget;

    private int numOfColides;
    private int numOfNavPoints;

    public GameObject avion;

    public GameObject canvas;

    private const string NAVIGATION_POINT1 = "navigationPoint1";
    private const string NAVIGATION_POINT = "navTocka";
    private const string NAVIGATION_POINT2 = "navigationPoint2";
    private const string AIRPLANE_USER = "cAirplane";
    private const string AIRPLANE_AI = "AirplaneAI";

    // Start is called before the first frame update
    void Start()
    {
canvas.SetActive(false);
    }


    // Update is called once per frame

    void FixedUpdate() {
    	avion.transform.position+=transform.up*speed;
    }

    void Update()
    {
            if(numOfNavPoints==5) {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
    }

    private void OnMouseDown()
    {
        if(canvas.activeInHierarchy==true)
             {
                //Debug.Log("Zatvori izbornik"); 
                canvas.SetActive(false);
             } 
        else if(canvas.activeInHierarchy==false) 
             {
                //Debug.Log("Otvori izbornik"); 
                canvas.SetActive(true);
            }
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(NAVIGATION_POINT))
        {
            GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();

            numOfNavPoints++;
            Debug.Log("Prolaz kroz nav tocku "+tekst.text);
            Debug.Log("Prodeni broj tocaka "+numOfNavPoints);
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
        }

                if (collision.gameObject.CompareTag(AIRPLANE_AI))
        {

            GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();

            Debug.Log("Sudar sa "+tekst.text);
            gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);

                                    numOfColides++;
            Debug.Log("Trenutni broj sudara "+numOfColides);
        }

    }

        void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
    }

}
