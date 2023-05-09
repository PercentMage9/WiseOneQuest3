using System.Collections;
using System.Collections.Generic;
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
    public GameObject FightPanel;
    public GameObject Button1Text;
    public GameObject Button2Text;
    public GameObject Button3Text;
    public GameObject Button4Text;

    //Attack names array for assigning to button text
    string[][] attackNames = new string[][]
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
    public Slider PlayerHealthBar;
    public Slider EnemyHealthBar;
    public int MinHealth;                   //Having these makes it easier to change the min/max values while in unity editor
    public int MaxHealth;


    public void WizardInitialize(Wizard wizard,Slider healthBar)
    {

        //Element setup
        var RandNumber = Random.Range(0, 3);             //Pick a random number between 0 and 3
        wizard.element = (Elements)RandNumber;                 //Set enemy element value to that number

        //Wisdom dexterity setup
        wizard.dexterity = Random.Range(minWisDexStat, maxWisDexStat);             //Set player dexterity to a random value between these values
        wizard.wisdom = Random.Range(minWisDexStat, maxWisDexStat);             //Set player wisdom to a random value between these values

        //Health setup
        //Creates random integers between given variables, divides by 10, rounds and then times by 10 to get closest 10
        int PlayerRandInt = Mathf.RoundToInt(Random.Range(MinHealth, MaxHealth) / 10) * 10;
        int EnemyRandInt = Mathf.RoundToInt(Random.Range(MinHealth, MaxHealth) / 10) * 10;

        //Set their health max values
        healthBar.maxValue = PlayerRandInt;

        //Set their current health to the max value
        healthBar.value = PlayerHealthBar.maxValue;
    }

    //On start
    void Start()
    {
        playerWizard = new Wizard();
        enemyWizard = new Wizard();

        WizardInitialize(playerWizard, PlayerHealthBar);
        WizardInitialize(enemyWizard, EnemyHealthBar);
    }

    //For setting the player's element depending on what element is selected
    public void SetPlayerElement()
    {
        var elementsIndex = elementList.value;

        //Assign the text in the buttons to the values in the arrays dependant on what element the player is
        Button1Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[elementsIndex][0];
        Button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[elementsIndex][1];
        Button3Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[elementsIndex][2];
        Button4Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[elementsIndex][3];

        playerWizard.element = (Elements)elementsIndex;
    }

    //For starting the game
    public void StartGame()
    {
        //Start the fight!!
        if (playerWizard.dexterity < enemyWizard.dexterity)       //If the enemy has higher dexterity
        {
            FightPanel.SetActive(false);
            chosenAttack = Random.Range(0, 3);
            WizardTurn(enemyWizard,playerWizard,chosenAttack);
        }
        else                                        //If the player has equal to or higher dexterity
        {
            FightPanel.SetActive(true);
            chosenAttack = Random.Range(0, 3);
            WizardTurn(playerWizard,enemyWizard,chosenAttack);
        }
    }

    public void RunWizardTurn(int chosenAttack)
    {
        WizardTurn(playerWizard,enemyWizard,chosenAttack);
    }

    //Wizards, they fight!
    public void WizardTurn(Wizard attackingWizard,Wizard defendingWizard,int chosenAttack)
    {
        FightPanel.SetActive(false);        //Close the fight panel, done to make sure its closed
        StartCoroutine(WaitAndPrint(3.0f));                 //Pause for 3 seconds

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

        PlayerHealthBar.value = (PlayerHealthBar.value - damageToDeal);
        Debug.Log("Enemy turn ended!");
        Debug.Log(FightPanel.activeInHierarchy);
        FightPanel.SetActive(FightPanel.activeInHierarchy == false);
        Debug.Log(FightPanel.activeInHierarchy);
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