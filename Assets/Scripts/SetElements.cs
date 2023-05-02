using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetElements : MonoBehaviour
{
    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air

    public int playerElementValue;
    public int enemyElementValue;
    public TMP_Dropdown elementList;

    void Start()
    {
        var RandNumber = Random.Range(0,3);             //Pick a random number between 0 and 3
        enemyElementValue = RandNumber;                 //Set enemy element value to that number
    }

    //For setting the player's element depending on what element is selected
    public void setPlayerElement()
    {
        playerElementValue = elementList.value;
    }
}