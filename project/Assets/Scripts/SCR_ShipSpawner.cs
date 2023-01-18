using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SCR_ShipSpawner : MonoBehaviour
{
    [SerializeField]
    private ReadConfigFile configFile;

    public GameObject game_area;
    public GameObject ship_prefab;
 
    public int ship_count = 0;
    private int ship_limit;
    public int ships_per_frame = 1;
 
    public float spawn_circle_radius = 80.0f;
    public float death_circle_radius = 90.0f;
 
    public float fastest_speed = 12.0f;
    public float slowest_speed = 0.75f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        ship_limit = configFile.getNumOfUncontrollablePlains();
    }
 
    void Update()
    {
        MaintainPopulation();
    }
 
 
    void MaintainPopulation()
    {
        if(ship_count < ship_limit)
        {
            for(int i=0; i<ships_per_frame; i++)
            {
                Vector3 position = GetRandomPosition(false);
                SCR_Ship ship_script = AddShip(position);
                ship_script.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f));
            }
        }
    }
 
    Vector3 GetRandomPosition(bool within_camera)
    {
 
        Vector3 position = Random.insideUnitCircle;
 
        if(within_camera == false)
        {
            position = position.normalized;
        }
 
        position *= spawn_circle_radius;
        position += game_area.transform.position;
 
        return position;
    }
 
    SCR_Ship AddShip(Vector3 position)
    {
        /**Add a new ship to the game and set the basic attributes. **/
 
        ship_count += 1;
        GameObject new_ship = Instantiate(
            ship_prefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (game_area.transform.position-position)),
            gameObject.transform
        );

            GameObject T = new_ship.transform.GetChild(0).gameObject;
            TextMesh tekst = T.GetComponent<TextMesh>();
            tekst.text="";
            int oznaka = Random.Range(1000, 9999);
            
            switch(oznaka%8) 
                    {

                        case 0: 
                            tekst.text+= "AFR ";
                            break;
                        case 1: 
                            tekst.text+= "EIN ";
                            break;
                        case 2: 
                            tekst.text+= "BAW ";
                            break;
                        case 3: 
                            tekst.text+= "UAE ";
                            break;
                        case 4: 
                            tekst.text+= "CTN ";
                            break;
                        case 5: 
                            tekst.text+= "KLM ";
                            break;
                        case 6: 
                            tekst.text+= "DLH ";
                            break;
                        case 7: 
                            tekst.text+= "QTR ";
                            break;
                        case 8: 
                            tekst.text+= "USA ";
                            break;
                        default: 
                            tekst.text+= "JZ ";
                            break;

                    }

            tekst.text+=oznaka.ToString();
 
        SCR_Ship ship_script = new_ship.GetComponent<SCR_Ship>();
        ship_script.ship_spawner = this;
        ship_script.game_area = game_area;
        ship_script.speed = Random.Range(slowest_speed, fastest_speed);
 
        return ship_script;
    }
}