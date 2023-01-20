using UnityEngine;
using System.Collections.Generic;

public class ControllableAirplane : MonoBehaviour
{
    private float speed = 0.002f;

    private const float turnSpeed = 6.0f;

    private Rigidbody2D myBody;

    private Timer timer;

    private int numOfNavPoints;

    public GameObject avion;

    public GameObject canvas;
    public GameObject nameHolder;

    private const string NAVIGATION_POINT = "navTocka";

    private const string AIRPLANE_USER = "cAirplane";
    
    private const string AIRPLANE_AI = "AirplaneAI";

    private WriteData myObject;

    private ColiderCounter coliderCounter;

    private ReadConfigFile configFile;

    [SerializeField]
    public turnDropdown dropdown;
        [SerializeField]
    public secondNumberDropdown dropdownsecond;
        [SerializeField]
    public firstNumberDropdown dropdownfirst;
    
    private float anglesTravelled = 0f;

    private bool travel = true;

    private string airplaneName;

    private int[] order;
        
   private navPoints navPoints;

    private List<Vector2> navPointsPosition;

    private int nextPointIndex;

    private int orderIndex;

    private float x;
    private float y;

    private Vector3 mDirection;


    private float fi;
     private float xNovi;
    private float yNovi;

    private float xStari;

    private float yStari;

    private bool proslo = false;
    

    void Start()
    {
        GameObject G = GameObject.Find(airplaneName);
        myBody = G.GetComponent<Rigidbody2D>();

        Canvas c = myBody.GetComponent<Canvas>();
        Debug.Log(c);

        G = GameObject.Find("WriteData");
        myObject = G.GetComponent<WriteData>();
        
        G = GameObject.Find("ColliderCounter");
        coliderCounter = G.GetComponent<ColiderCounter>();

        G = GameObject.Find("Config");
        configFile = G.GetComponent<ReadConfigFile>();

        G = GameObject.Find("Timer");
        timer = G.GetComponent<Timer>();

        canvas.SetActive(false);

        orderIndex = 0;
        nextPointIndex = order[orderIndex] - 1;

	    G = GameObject.Find("navPoints");
        navPoints = G.GetComponent<navPoints>();

        navPointsPosition =navPoints.getAllNavPositions();

        myObject.writeLog(string.Format("[{0}] Airplane {1} is at position ({2},{3}) in direction {4}.",Timer.dateTime,airplaneName, myBody.transform.position.x, myBody.transform.position.y,mDirection));

    }



    void FixedUpdate() {
    	avion.transform.position+=transform.up*speed;
    }

    void Update()
    {
        move();
        checkTimer();
    }

    private void checkTimer() {

        if (timer.getRemaining() < 0) {
                
                        if(dropdown.getNum() != 0) {
                  xNovi=Mathf.Abs(Mathf.Cos(dropdown.getNum()*Mathf.PI/180));
                 yNovi=Mathf.Abs(Mathf.Sin(dropdown.getNum()*Mathf.PI/180)); 
                 xStari = myBody.transform.position.x;
                 yStari = myBody.transform.position.y;
                 float duljinaNovi = Mathf.Sqrt(xNovi*xNovi + yNovi*yNovi);
                 float duljinaStari = Mathf.Sqrt(xStari*xStari + yStari*yStari);
                 fi =  Mathf.Acos((xNovi * xStari + yNovi * yStari)/(duljinaNovi*duljinaStari));
                 x = Mathf.Abs(Mathf.Cos(fi));
                 y = Mathf.Abs(Mathf.Sin(fi));
                mDirection = new Vector3(x, y, 0);
                myObject.writeLog(string.Format("[{0}] Airplane {1} is at position ({2},{3}) in direction {4}.",Timer.dateTime,airplaneName, myBody.transform.position.x, myBody.transform.position.y, mDirection));
            }else{
                 myObject.writeLog(string.Format("[{0}] Airplane {1} is at position ({2},{3}) in direction {4}.",Timer.dateTime,airplaneName, myBody.transform.position.x, myBody.transform.position.y,mDirection));
            }

            Timer.timesWritten++;
            if (Timer.timesWritten == configFile.getNumOfControllablePlanes()) {
                Timer.timesWritten = 0;
                timer.setTimeRemaining(5.0f);
            }
        }
    
    }


    private void move() {

        if (dropdown.getChanged())
        {
            travel = true;
              
        }


        if (!travel)
        {

            myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
            return;
        }
        if (dropdown.getNum() != 0)
        {
          

            if (dropdown.getChanged())
            {
                if(!proslo) {
                                GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
                myObject.writeLog(string.Format("[{0}] Turn airplane {1} to {2} for {3}deg.",Timer.dateTime,tekst.text ,dropdown.getTurn() ,dropdown.getNum()));
                  proslo = true;
                }
              
                if (dropdown.getTurn() == "Right")
                {
                    anglesTravelled += Time.deltaTime * turnSpeed;
                    myBody.transform.localEulerAngles += new Vector3(0f, 0f, -Time.deltaTime * turnSpeed);
                    myBody.transform.Translate(transform.up * speed * Time.deltaTime);
                    canvas.transform.rotation = Quaternion.identity;
                    nameHolder.transform.rotation = Quaternion.identity;
                    //stariNum = turnDropdown.num;
                }
                else
                {
                    anglesTravelled += Time.deltaTime * turnSpeed;
                    myBody.transform.localEulerAngles -= new Vector3(0f, 0f, -Time.deltaTime * turnSpeed);
                    myBody.transform.Translate(transform.up * speed * Time.deltaTime);
                    canvas.transform.rotation = Quaternion.identity;
                    nameHolder.transform.rotation = Quaternion.identity;
                }
                
            }
            else
            {
                myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
                canvas.transform.rotation = Quaternion.identity;
                nameHolder.transform.rotation = Quaternion.identity;
            }

            if (Mathf.Abs(anglesTravelled) > dropdown.getNum())
            {
                travel = false;
                anglesTravelled = 0f;
                myBody.transform.Translate(transform.up * speed * Time.deltaTime);
                dropdown.setChanged(false);
                dropdown.setTurn("-");
                dropdownfirst.setTurnFirst("-");
                dropdownsecond.setTurnSecond("-");
                canvas.transform.rotation = Quaternion.identity;
                nameHolder.transform.rotation = Quaternion.identity;
                proslo = false;
            }

        }
        else
        {
            myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
            canvas.transform.rotation = Quaternion.identity;
            nameHolder.transform.rotation = Quaternion.identity;
        }
    }

    private void OnMouseDown()
    {       
            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.MM-HH:mm:ss:fff");
            string log = string.Format("[{0}] You clicked on {1}", dateTime,tekst.text);
            myObject.writeLog(log);

        if(canvas.activeInHierarchy) canvas.SetActive(false);  
        else canvas.SetActive(true);
            
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(NAVIGATION_POINT))
        {
 		    Vector2 airplanePosition = new Vector2(myBody.transform.position.x, myBody.transform.position.y);
            Vector2 nextNavPointDirection = navPointsPosition[nextPointIndex];

            double distance = Mathf.Sqrt(Mathf.Pow(airplanePosition.x - nextNavPointDirection.x, 2) + Mathf.Pow(airplanePosition.y - nextNavPointDirection.y, 2));

            if (distance > 1) return;

            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
                myObject.writeLog(string.Format("[{0}] Airplane {1} passed through nav point {2}.",Timer.dateTime,tekst.text , nextPointIndex+1) );

            numOfNavPoints++;
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);

            checkToDestroy();
        }

                if (collision.gameObject.CompareTag(AIRPLANE_AI))
        {

            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            GameObject T1 = collision.transform.GetChild(0).gameObject;
            TextMesh tekstTocke = T1.GetComponent<TextMesh>();

            gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);

            myObject.writeLog(string.Format("[{0}] Airplane {1} collided with {2}.",Timer.dateTime,tekst.text , tekstTocke.text) );

            coliderCounter.increaseCounter();
        }

                if (collision.gameObject.CompareTag(AIRPLANE_USER))
        {
            
            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            GameObject T1 = collision.transform.GetChild(0).gameObject;
            TextMesh tekstTocke = T1.GetComponent<TextMesh>();

            /*GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();*/
             myObject.writeLog(string.Format("[{0}] Airplane {1} collided with {2}.",Timer.dateTime,tekst.text , tekstTocke.text) );

            gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);

            coliderCounter.increaseCounter();

        }

    }

        void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
    }

    private void checkToDestroy() {
        if (numOfNavPoints == configFile.getNumOfNavPoints())
        {
            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            myObject.writeLog(string.Format("[{0}] Airplane {1} passed through all navigation points.",Timer.dateTime,tekst.text) );
            Destroy(avion);
            coliderCounter.increaseDestroyedAirplanes();
        }else {
            orderIndex++;
            nextPointIndex = order[orderIndex] - 1;
        }

        if (coliderCounter.getDestroyedAirplanes() == configFile.getNumOfControllablePlanes())
        {
            Application.Quit();
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.MM-HH:mm:ss:fff");
            string log = string.Format("[{0}] Simulation finished, total number of collides is {1} ", dateTime, coliderCounter.getCounter());
            myObject.writeLog(log);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void setOrder(int[] order)
    {
        this.order = order;
    }

    public void setName(string name) {
        airplaneName = name;
    }

    public void setDirection(Vector3 mDirection){
        this.mDirection = mDirection;
    }

}
