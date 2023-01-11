using UnityEngine;

public class ControllableAirplane : MonoBehaviour
{
    private float speed = 0.005f;

    private const float turnSpeed = 12.0f;

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
    
    private float anglesTravelled = 0f;

    private bool travel = true;

    private string airplaneName;
    

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
                
            myObject.writeLog(string.Format("[{0}] Airplane {1} is at position ({2},{3}).",Timer.dateTime,airplaneName, myBody.transform.position.x, myBody.transform.position.y));

            Timer.timesWritten++;
            if (Timer.timesWritten == configFile.getNumOfControllablePlains()) {
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
            GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();

            numOfNavPoints++;
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);

            checkToDestroy();
        }

                if (collision.gameObject.CompareTag(AIRPLANE_AI))
        {

            GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();

            gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);

            coliderCounter.increaseCounter();
        }

                if (collision.gameObject.CompareTag(AIRPLANE_USER))
        {

            GameObject T = collision.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();

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
            Destroy(avion);
            coliderCounter.increaseDestroyedAirplanes();
        }

        if (coliderCounter.getDestroyedAirplanes() == configFile.getNumOfControllablePlains())
        {
            Application.Quit();
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.MM-HH:mm:ss:fff");
            string log = string.Format("[{0}] Simulation finished, total number of collides is {1} ", dateTime, coliderCounter.getCounter());
            myObject.writeLog(log);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void setName(string name) {
        airplaneName = name;
    }

}
