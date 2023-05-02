using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetElements : MonoBehaviour
{
    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air
    //Element values were set as strings to act nicer with the drop down menu

    public string playerElementValue;
    public string enemyElementValue;
    public GameObject elementList;

    void Start()
    {
        var RandNumber = Random.Range(0,3);             //Pick a random number between 0 and 3
        enemyElementValue = RandNumber.ToString();                 //Set enemy element value to that number as a string
    }

    //For setting the player's element depending on what element is selected
    public void setPlayerElement()
    {
       // playerElementValue = elementList.GetComponent<TMPro.TextMeshProUGUI>().m_Value;
       playerElementValue = TMPro.TMP_Dropdown[] elementList = GetComponentsInChildren<TMPro.TMP_Dropdown>().value;
    }
}

