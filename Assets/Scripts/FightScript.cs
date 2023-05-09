using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Ethan's stupid enums
public enum Elements
{
    Fire,
    Water,
    Earth,
    Air
}

public class FightScript : MonoBehaviour
{
    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air

    public Wizard playerWizard,enemyWizard;

    //Game objects
    public GameObject fightPanel;
    public GameObject screens;
    public GameObject winnerScreen;

    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public TextMeshProUGUI button3Text;
    public TextMeshProUGUI button4Text;
    public TextMeshProUGUI winnerText;

    //Attack names array for assigning to button text
    public string[][] attackNames = new string[][]
    {
    new string[]{"Fiery Fiasco","Inferno Insanity","Flame Frenzy","Burnout Blitz"},
    new string[]{"Aqua Assault","Splash Attack","Water Wallop","Bubble Blast"},
    new string[]{"Dirt Devilry","Boulderdash Blitz","Pebble Pummel","Soil Slam"},
    new string[]{"Whirlwind Whack","Airborne Assault","Tempest Takedown","Breeze Blast"}
    };

    //Integer values for the base damage of each attack
    public int attack1BaseDamage;
    public int attack2BaseDamage;
    public int attack3BaseDamage;
    public int attack4BaseDamage;

    //Element setup
    public TMP_Dropdown elementList;

    //Wisdom dexterity setup
    public int minWisDexStat;                    //Minimum value any of these stats can be
    public int maxWisDexStat;                    //Maximum value any of these stats can be

    //Attack variables
    public int damageToDeal = 0;
    public int chosenAttack = 0;

    //Health setup
    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public int minHealth;                   //Having these makes it easier to change the min/max values while in unity editor
    public int maxHealth;

    private bool isItPlayersTurn;

    public void WizardInitialize(Wizard wizard,Slider healthBar)
    {

        //Element setup
        var RandNumber = Random.Range(0, 3);             //Pick a random number between 0 and 3
        wizard.element = (Elements)RandNumber;                 //Set enemy element value to that number

        //Wisdom dexterity setup
        wizard.dexterity = Random.Range(minWisDexStat, maxWisDexStat);             //Set player dexterity to a random value between these values
        wizard.wisdom = Random.Range(minWisDexStat, maxWisDexStat);             //Set player wisdom to a random value between these values

        //Health setup
        //Creates random integer between given variables, divides by 10, rounds and then times by 10 to get closest 10
        int healthRandInt = Mathf.RoundToInt(Random.Range(minHealth, maxHealth) / 10) * 10;

        //Set their current health to the max value
        wizard.maxHealth = healthRandInt;
        wizard.currentHealh = wizard.maxHealth;
    }

    //On start
    void Start()
    {
        playerWizard = new Wizard();
        enemyWizard = new Wizard();

        WizardInitialize(playerWizard, playerHealthBar);
        WizardInitialize(enemyWizard, enemyHealthBar);
    }

    //For setting the player's element depending on what element is selected
    public void SetPlayerElement()
    {
        var elementsIndex = elementList.value;

        //Assign the text in the buttons to the values in the arrays dependant on what element the player is
        button1Text.text = attackNames[elementsIndex][0];
        button2Text.text = attackNames[elementsIndex][1];
        button3Text.text = attackNames[elementsIndex][2];
        button4Text.text = attackNames[elementsIndex][3];

        playerWizard.element = (Elements)elementsIndex;
    }

    public void Update()
    {
        playerHealthBar.maxValue = playerWizard.maxHealth;
        enemyHealthBar.maxValue = enemyWizard.maxHealth;

        playerHealthBar.value = playerWizard.currentHealh;
        enemyHealthBar.value = enemyWizard.currentHealh;
    }

    //For starting the game
    public void StartGame()
    {
        //Start the fight!!
        if (playerWizard.dexterity >= enemyWizard.dexterity)
        {
            isItPlayersTurn = true;
        }
        else
        {
            isItPlayersTurn = false;
        }

        StartWizardTurn();
    }

    public void RunWizardAttack(int chosenAttack)
    {
        //Start the fight!!
        if (isItPlayersTurn == true)
        {
            fightPanel.SetActive(true);
            StartCoroutine(WizardAttack(playerWizard, enemyWizard, chosenAttack));
        }
        else
        {
            fightPanel.SetActive(false);
            chosenAttack = Random.Range(0, 3);
            StartCoroutine(WizardAttack(enemyWizard, playerWizard, chosenAttack));
        }
    }

    public void StartWizardTurn()
    {
        if (enemyWizard.currentHealh <= 0)
        {
            GameOver(true);
            return;
        }
        else if (playerWizard.currentHealh <= 0)
        {
            GameOver(false);
            return;
        }


        if(isItPlayersTurn == true) 
        {
            fightPanel.SetActive(true);
        }
        else 
        { 
            fightPanel.SetActive(false);
            RunWizardAttack(chosenAttack);
        }
    }

    public void GameOver(bool hasPlayerWon)
    {
        screens.SetActive(false);
        winnerText.text = (hasPlayerWon ? playerWizard.name : enemyWizard.name) + " has won!";
        winnerScreen.SetActive(true);
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //Wizards, they fight!
    public IEnumerator WizardAttack(Wizard attackingWizard,Wizard defendingWizard,int chosenAttack)
    {
        fightPanel.SetActive(false);        //Close the fight panel, done to make sure its closed
        yield return new WaitForSeconds(1);
        Debug.Log("Done waiting!");

        //Determine the damage to deal
        if (chosenAttack == 0)
        {
            damageToDeal = (attackingWizard.dexterity + attack1BaseDamage);
        }
        else if (chosenAttack == 1)
        {
            damageToDeal = (attackingWizard.dexterity + attack2BaseDamage);
        }
        else if (chosenAttack == 2)
        {
            damageToDeal = (attackingWizard.dexterity + attack3BaseDamage);
        }
        else if (chosenAttack == 3)
        {
            damageToDeal = (attackingWizard.dexterity + attack4BaseDamage);
        }
        else        //Fallback for incase the chosen attack somehow equals something else
        {
            damageToDeal = (attackingWizard.dexterity + 20);
        }

        //Multiply damage depending on element type
        if (attackingWizard.element == Elements.Fire && defendingWizard.element == Elements.Air)
        {
            MultiplyDamage();
        }
        else if (attackingWizard.element == Elements.Water && defendingWizard.element == Elements.Fire)
        {
            MultiplyDamage();
        }
        else if (attackingWizard.element == Elements.Earth && defendingWizard.element == Elements.Water)
        {
            MultiplyDamage();
        }
        else if (attackingWizard.element == Elements.Air && defendingWizard.element == Elements.Earth)
        {
            MultiplyDamage();
        }

        defendingWizard.currentHealh = (defendingWizard.currentHealh - damageToDeal);
        isItPlayersTurn = !isItPlayersTurn;
        StartWizardTurn();
        Debug.Log("Turn ended!");
    }

    public void MultiplyDamage()
    {
        damageToDeal = (int)(damageToDeal * 1.5f);
    }

    //Function for pausing
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Done waiting!");
    }
}