using UnityEngine;

public class ControllableAirplane : MonoBehaviour
{
    private float speed = 0.005f;

    private const float turnSpeed = 12.0f;

    private Rigidbody2D myBody;

    private int numOfNavPoints;

    public GameObject avion;

    public GameObject canvas;

    private const string NAVIGATION_POINT = "navTocka";

    private const string AIRPLANE_USER = "cAirplane";
    
    private const string AIRPLANE_AI = "AirplaneAI";

    private WriteData myObject;

    private ColiderCounter coliderCounter;

    private ReadConfigFile configFile;

    private float anglesTravelled = 0f;

    private bool travel = true;

    private string airplaneName;
    void Start()
    {
        GameObject G = GameObject.Find(airplaneName);
        myBody = G.GetComponent<Rigidbody2D>();

        G = GameObject.Find("WriteData");
        myObject = G.GetComponent<WriteData>();
        
        G = GameObject.Find("ColliderCounter");
        coliderCounter = G.GetComponent<ColiderCounter>();

        G = GameObject.Find("Config");
        configFile = G.GetComponent<ReadConfigFile>();

        canvas.SetActive(false);

    }



    void FixedUpdate() {
    	avion.transform.position+=transform.up*speed;
    }

    void Update()
    {

        if (turnDropdown.changed)
        {
            travel = true;
        }


        if (!travel)
        {

            myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
            return;
        }
        if (turnDropdown.num != 0)
        {

            if (turnDropdown.changed)
            {
                if (turnDropdown.turn == "Right")
                {
                    anglesTravelled += Time.deltaTime * turnSpeed;
                    myBody.transform.localEulerAngles += new Vector3(0f, 0f, -Time.deltaTime * turnSpeed);
                    myBody.transform.Translate(transform.up * speed * Time.deltaTime);

                    //stariNum = turnDropdown.num;
                }
                else
                {
                    anglesTravelled += Time.deltaTime * turnSpeed;
                    myBody.transform.localEulerAngles -= new Vector3(0f, 0f, -Time.deltaTime * turnSpeed);
                    myBody.transform.Translate(transform.up * speed * Time.deltaTime);
                }
            }
            else
            {
                myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (Mathf.Abs(anglesTravelled) > turnDropdown.num)
            {
                travel = false;
                anglesTravelled = 0f;
                myBody.transform.Translate(transform.up * speed * Time.deltaTime);
                turnDropdown.changed = false;
                turnDropdown.turn = "-";
            }

        }
        else
        {
            myBody.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {       
            GameObject T = avion.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.mm-HH:mm:ss:fff");
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
            string dateTime = System.DateTime.Now.ToString("yyyy.dd.mm-HH:mm:ss:fff");
            string log = string.Format("[{0}] Simulation finished, total number of collides is {1} ", dateTime, coliderCounter.getCounter());
            myObject.writeLog(log);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void setName(string name) {
        airplaneName = name;
    }

}
