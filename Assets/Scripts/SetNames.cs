using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNames : MonoBehaviour
{
    //Create variables for GameObjects and the array for enemy names
    public GameObject EnemyNameText;
    public GameObject PlayerNameText;
    public GameObject PlayerNameTextBox;
    public string[] EnemyNameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    void Start()
    {
        var RandNumber = Random.Range(0,9);                                                             //Select a random value between 0 and 9
        EnemyNameText.GetComponent<TMPro.TextMeshProUGUI>().text = EnemyNameList[RandNumber];           //Set the enemy name text to the value in the array
    }

    //For setting the player name
    public void setPlayerName()
    {
        //Set the player name text to the text inside the name enter text box
        PlayerNameText.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerNameTextBox.GetComponent<TMPro.TextMeshProUGUI>().text;       
    }
}