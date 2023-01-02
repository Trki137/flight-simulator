using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderCounter : MonoBehaviour
{
    private int counter=0;
    private int numOfDestroyedAirplains=0;

    public void increaseCounter(){
        counter++;
    }

    public int getCounter() {
        return counter;
    }

    public void increaseDestroyedAirplanes(){
        numOfDestroyedAirplains++;
    }

    public int getDestroyedAirplanes() {
        return numOfDestroyedAirplains;
    }


}
