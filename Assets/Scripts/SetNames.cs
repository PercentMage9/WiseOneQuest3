//Oh boy, here we go again
//Including this in the fight script would have been too much
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNames : MonoBehaviour
{
    //GameObjects n stuff
    public GameObject elementPanel;
    public FightScript fightScript;
    public Button button;

    //TMP, it exists, sure why not
    public TMP_InputField playerNameTextBox;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI errorText;
    
    //I'm gonna add "olimar" and then get sued
    public string[] enemyNameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    //Does stuff before the program actually really "starts", pretty cool, thanks Awake
    public void Awake()
    {
        SetButtonState(playerNameTextBox.text);
        playerNameTextBox.onValueChanged.AddListener(SetButtonState);
    }

    //I dont remember why I put this here but it has something to do with checking the name length in the text box
    public void SetButtonState(string enteredName) 
    { 
        button.interactable = (enteredName.Length > 0 && enteredName.Length <= 16); 
    }

    //For setting the player name
    public void SetPlayerName()
    {
        var randNumber = Random.Range(0, enemyNameList.Length - 1);
        enemyNameText.text = enemyNameList[randNumber];
        fightScript.enemyWizard.name = enemyNameText.text;

        string enteredName = playerNameTextBox.text;

        if (enteredName.Length > 0 && enteredName.Length <= 16)
        {
            errorText.gameObject.SetActive(false);
            gameObject.SetActive(false);
            elementPanel.SetActive(true);
            playerNameText.text = enteredName;
            fightScript.playerWizard.name = enteredName;
        }
        else
        {
            //Fallback for if the player somehow breaks the button hiding stuff
            //Not sure how but I once broke it and inputted a name that was like 50
            //characters long, thats why this is here
            errorText.gameObject.SetActive(true);
            errorText.text = "Invalid name length!";
        }
    }
}
