using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableAirplane : MonoBehaviour
{
    private float speed = 0.01f;

    private Rigidbody2D myBody;

    private Transform[] navigationPoint = new Transform[2];
    private Transform currentTarget;

    private float x;
    private float y;
    private int numOfColides;

    private const string NAVIGATION_POINT1 = "navigationPoint1";
    private const string NAVIGATION_POINT2 = "navigationPoint2";
    private const string AIRPLANE_TAG = "cAirplane";


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

   

        navigationPoint[0] = currentTarget = GameObject.FindWithTag(NAVIGATION_POINT1).transform;
        navigationPoint[1] = GameObject.FindWithTag(NAVIGATION_POINT2).transform;

        
        x = currentTarget.position.x - myBody.position.x;
        y = currentTarget.position.y - myBody.position.y;

        numOfColides = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(x, y, 0);
        myBody.transform.Translate(v * speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Debug.Log("Sprite Clicked");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(NAVIGATION_POINT1))
        {
            currentTarget = navigationPoint[1];
            x = currentTarget.position.x - myBody.position.x;
            y = currentTarget.position.y - myBody.position.y;
        }

        if (collision.gameObject.CompareTag(NAVIGATION_POINT2)) {
            x = y = 0;
            Debug.Log("Simulation ended");
            Debug.Log(numOfColides);
        }

        if (collision.gameObject.CompareTag(AIRPLANE_TAG)) {
            Debug.Log("Airplanes collide");
            numOfColides++;
        }

    }

}
