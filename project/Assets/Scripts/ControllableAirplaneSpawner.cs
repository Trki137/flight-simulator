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
        for (int i = 0; i < configFile.getNumOfControllablePlains() ; i++) 
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
            
        string airplaneName = "HR " + Random.Range(1000, 9999).ToString();

        textMesh.text = airplaneName;

        ControllableAirplane airplaneScript = airplane.GetComponent<ControllableAirplane>();
        airplaneScript.name = airplaneName;
        airplaneScript.setName(airplaneName);

        airplaneScript.setOrder(configFile.getOrderForIndex(index));


    }



}