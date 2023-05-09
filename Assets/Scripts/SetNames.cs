using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNames : MonoBehaviour
{
    //Create variables for GameObjects and the array for enemy names
    public GameObject elementPanel;                      //Refers to the element enter form
    public FightScript fightScript;
    public Button button;

    public TMP_InputField playerNameTextBox;            //Refers to the box the player enters their name into
    public TextMeshProUGUI enemyNameText;                //Refers to the text above the enemy's head
    public TextMeshProUGUI playerNameText;               //Refers to the text above the player's head
    public TextMeshProUGUI errorText;                    //Refers to the error text on the name enter form
    
    public string[] enemyNameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    public void Awake()
    {
        SetButtonState(playerNameTextBox.text);
        playerNameTextBox.onValueChanged.AddListener(SetButtonState);
    }

    public void SetButtonState(string enteredName) 
    { 
        button.interactable = (enteredName.Length > 0 && enteredName.Length <= 16); 
    }

    //For setting the player name
    public void SetPlayerName()
    {
        var randNumber = Random.Range(0, enemyNameList.Length - 1);                       //Select a random value between 0 and 9
        enemyNameText.text = enemyNameList[randNumber];                                   //Set the enemy name text to the value in the array
        fightScript.enemyWizard.name = enemyNameText.text;

        string enteredName = playerNameTextBox.text;                    //Create a variable for the if statement

        //Needs fixing to not allow 0 length names
        if (enteredName.Length > 0 && enteredName.Length <= 16)         //Checks if the name is of appropriate length
        {
            errorText.gameObject.SetActive(false);
            gameObject.SetActive(false);
            elementPanel.SetActive(true);
            playerNameText.text = enteredName;                          //Store the player's name in the UI text
            fightScript.playerWizard.name = enteredName;
        }
        else
        {
            //Close the name enter form and open the element form
            errorText.gameObject.SetActive(true);
            errorText.text = "Invalid name length!";
        }
    }
}