using TMPro;
using UnityEngine;

public class SetNames : MonoBehaviour
{
    //Create variables for GameObjects and the array for enemy names
    public TextMeshProUGUI enemyNameText;                //Refers to the text above the enemy's head
    public TextMeshProUGUI playerNameText;               //Refers to the text above the player's head
    public TMP_InputField playerNameTextBox;            //Refers to the box the player enters their name into
    public TextMeshProUGUI errorText;                    //Refers to the error text on the name enter form
    public FightScript fightScript;
    public GameObject elementPanel;                      //Refers to the element enter form
    public string[] enemyNameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    //For setting the player name
    public void SetPlayerName()
    {
        var RandNumber = Random.Range(0, enemyNameList.Length - 1);                       //Select a random value between 0 and 9
        enemyNameText.text = enemyNameList[RandNumber];           //Set the enemy name text to the value in the array
        fightScript.enemyWizard.name = enemyNameText.text;

        string EnteredName = playerNameTextBox.text;                    //Create a variable for the if statement

        //Needs fixing to not allow 0 length names
        if (EnteredName.Length > 0 && EnteredName.Length <= 16)         //Checks if the name is of appropriate length
        {
            errorText.gameObject.SetActive(false);
            gameObject.SetActive(false);
            elementPanel.SetActive(true);
            playerNameText.text = EnteredName;                          //Store the player's name in the UI text
            fightScript.playerWizard.name = EnteredName;
        }
        else
        {
            //Close the name enter form and open the element form
            errorText.gameObject.SetActive(true);
            errorText.text = "Invalid name length!";
        }
    }
}