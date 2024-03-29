using UnityEngine;
using System.Collections;

public class ControllableAirplaneSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject gameArea;

    [SerializeField]
    private GameObject airplanePrefab;

    [SerializeField]
    private ReadConfigFile configFile;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < configFile.getNumOfControllablePlanes() ; i++) 
            InstantianteAirplane(GetRandomPosition(),i);
    }

    Vector3 GetRandomPosition()
    {
        /** Get a random spawn position, using a 2D circle around the game area. **/

        float spawnY = Random.Range
                     (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector3 position = new Vector3(spawnX, spawnY, 1);

        return position;
    }

    private void InstantianteAirplane(Vector3 position,int index)
    {

       GameObject airplane = Instantiate(
            airplanePrefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (gameArea.transform.position - position)),
            gameObject.transform
         );

            
        GameObject T = airplane.transform.GetChild(0).gameObject;    
        TextMesh textMesh = T.GetComponent<TextMesh>();
            
        string airplaneName = "HR " + Random.Range((index+1)*1000, ((index+2)*1000)-1).ToString();

        textMesh.text = airplaneName;

        ControllableAirplane airplaneScript = airplane.GetComponent<ControllableAirplane>();
        airplaneScript.name = airplaneName;
        airplaneScript.setName(airplaneName);
        airplaneScript.setDirection(gameArea.transform.position - position);
        airplaneScript.setOrder(configFile.getOrderForIndex(index));

        //ControllableAirplane.myObject.writeLog(string.Format("[{0}] Airplane {1} is at position ({2},{3}) in direction {4}.",Timer.dateTime,airplaneName, ControllableAirplane.myBody.transform.position.x, ControllableAirplane.myBody.transform.position.y, direction));


    }



}