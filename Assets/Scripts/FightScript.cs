using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightScript : MonoBehaviour
{
    public GameObject FightPanel;
    public GameObject Button1Text;
    public GameObject Button2Text;
    public GameObject Button3Text;
    public GameObject Button4Text;

    //Attack names for button cosmetics
    public string[] fireAttackNames = {"Fiery Fiasco","Inferno Insanity","Flame Frenzy","Burnout Blitz"};
    public string[] waterAttackNames = {"Aqua Assault","Splash Attack","Water Wallop","Bubble Blast"};
    public string[] earthAttackNames = {"Dirt Devilry","Boulderdash Blitz","Pebble Pummel","Soil Slam"};
    public string[] airAttackNames = {"Whirlwind Whack","Airborne Assault","Tempest Takedown","Breeze Blast"};

    //Integer values for the base damage of each attack
    public int attack1BaseDamage;
    public int attack2BaseDamage;
    public int attack3BaseDamage;
    public int attack4BaseDamage;

    //Element setup
    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air
    public int playerElementValue;
    public int enemyElementValue;
    public TMP_Dropdown elementList;

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
    }

    //For when its the player's turn
    public void playerTurn()
    {
        FightPanel.SetActive(true);         //Open the fight panel

    }
}