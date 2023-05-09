using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNames : MonoBehaviour
{
    //Create variables for GameObjects and the array for enemy names
    public GameObject EnemyNameText;                //Refers to the text above the enemy's head
    public GameObject PlayerNameText;               //Refers to the text above the player's head
    public GameObject PlayerNameTextBox;            //Refers to the box the player enters their name into
    public GameObject ErrorText;                    //Refers to the error text on the name enter form
    public GameObject Self;                         //Refers to the name enter form
    public GameObject ElementPanel;                 //Refers to the element enter form
    public string[] EnemyNameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    void Start()
    {
        var RandNumber = Random.Range(0,9);                                                             //Select a random value between 0 and 9
        EnemyNameText.GetComponent<TMPro.TextMeshProUGUI>().text = EnemyNameList[RandNumber];           //Set the enemy name text to the value in the array
    }

    //For setting the player name
    public void SetPlayerName()
    {
        string EnteredName = PlayerNameTextBox.GetComponent<TMPro.TextMeshProUGUI>().text;              //Create a variable for the if statement

        //Needs fixing to not allow 0 length names
        if (EnteredName.Length > 0 && EnteredName.Length <= 16)                                          //Checks if the name is of appropriate length
        {
            ErrorText.SetActive(false);
            Self.SetActive(false);
            ElementPanel.SetActive(true);
            PlayerNameText.GetComponent<TMPro.TextMeshProUGUI>().text = EnteredName;                    //Store the player's name in the UI text
        }
        else
        {
            //Close the name enter form and open the element form
            ErrorText.SetActive(true);
            ErrorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Invalid name length!";
        }
    }
}