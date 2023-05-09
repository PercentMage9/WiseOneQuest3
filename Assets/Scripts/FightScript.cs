using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightScript : MonoBehaviour
{
    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air
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
    public int playerElementValue;
    public int enemyElementValue;
    public TMP_Dropdown elementList;

    //Wisdom dexterity setup
    public int minWisDexStat;                    //Minimum value any of these stats can be
    public int maxWisDexStat;                    //Maximum value any of these stats can be
    public int playerDexterity;
    public int enemyDexterity;
    public int playerWisdom;
    public int enemyWisdom;

    //Attack variables
    public int damageToDeal = 0;
    public int chosenAttack = 0;

    //Health setup
    public Slider PlayerHealthBar;
    public Slider EnemyHealthBar;
    public int MinHealth;                   //Having these makes it easier to change the min/max values while in unity editor
    public int MaxHealth;

    //On start
    void Start()
    {
        //Element setup
        var RandNumber = Random.Range(0,3);             //Pick a random number between 0 and 3
        enemyElementValue = RandNumber;                 //Set enemy element value to that number

        //Wisdom dexterity setup
        playerDexterity = Random.Range(minWisDexStat,maxWisDexStat);             //Set player dexterity to a random value between these values
        enemyDexterity = Random.Range(minWisDexStat,maxWisDexStat);             //Set enemy dexterity to a random value between these values
        playerWisdom = Random.Range(minWisDexStat,maxWisDexStat);             //Set player wisdom to a random value between these values
        enemyWisdom = Random.Range(minWisDexStat,maxWisDexStat);             //Set enemy wisdom to a random value between these values

        //Health setup
        //Creates random integers between given variables, divides by 10, rounds and then times by 10 to get closest 10
        int PlayerRandInt = Mathf.RoundToInt(Random.Range(MinHealth,MaxHealth) / 10) * 10;
        int EnemyRandInt = Mathf.RoundToInt(Random.Range(MinHealth,MaxHealth) / 10) * 10; 

        //Set their health max values
        PlayerHealthBar.maxValue = PlayerRandInt;
        EnemyHealthBar.maxValue = EnemyRandInt;

        //Set their current health to the max value
        PlayerHealthBar.value = PlayerHealthBar.maxValue;
        EnemyHealthBar.value = EnemyHealthBar.maxValue;
    }

    //For setting the player's element depending on what element is selected
    public void setPlayerElement()
    {
        playerElementValue = elementList.value;

        //Assign the text in the buttons to the values in the arrays dependant on what element the player is
        Button1Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[playerElementValue][0];
        Button2Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[playerElementValue][1];
        Button3Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[playerElementValue][2];
        Button4Text.GetComponent<TMPro.TextMeshProUGUI>().text = attackNames[playerElementValue][3];
    }

    //For starting the game
    public void startGame()
    {
        //Start the fight!!
        if (playerDexterity < enemyDexterity)       //If the enemy has higher dexterity
        {
            enemyTurn();
        }
        else                                        //If the player has equal to or higher dexterity
        {
            playerTurn();
        }
    }

    //For when its the player's turn
    public void playerTurn()
    {
        FightPanel.SetActive(true);         //Open the fight panel
    }

    //For when its the enemy turn
    public void enemyTurn()
    {
        FightPanel.SetActive(false);        //Close the fight panel, done to make sure its closed
        StartCoroutine(WaitAndPrint(3.0f));                 //Pause for 3 seconds
        chosenAttack = Mathf.RandNumber(0,3);

        if (chosenAttack == 0)
        {
            damageToDeal = (enemyDexterity + attack1BaseDamage);
        }
        else if (chosenAttack == 1)
        {
            damageToDeal = (enemyDexterity + attack2BaseDamage);
        }
        else if (chosenAttack == 2)
        {
            damageToDeal = (enemyDexterity + attack3BaseDamage);
        }
        else if (chosenAttack == 3)
        {
            damageToDeal = (enemyDexterity + attack4BaseDamage);
        }
        else        //Fallback for incase the chosen attadck somehow equals something else
        {
            damageToDeal = (enemyDexterity + 20);
        }
    }

    //Function for pausing
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Done waiting!");
    }
}