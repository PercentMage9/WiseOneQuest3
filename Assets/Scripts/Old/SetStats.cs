using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetStats : MonoBehaviour
{

    //Note: elements go in order of: 0 - fire, 1 - water, 2 - earth, 3 - air

    public Slider PlayerHealthBar;
    public Slider EnemyHealthBar;
    public int MinHealth;                   //Having these makes it easier to change the min/max values while in unity editor
    public int MaxHealth;

    public int playerElementValue;
    public int enemyElementValue;
    public TMP_Dropdown elementList;

    void Start()
    {
        //Creates random integers between given variables, divides by 10, rounds and then times by 10 to get closest 10
        int PlayerRandInt = Mathf.RoundToInt(Random.Range(MinHealth,MaxHealth) / 10) * 10;
        int EnemyRandInt = Mathf.RoundToInt(Random.Range(MinHealth,MaxHealth) / 10) * 10; 

        //Set their health max values
        PlayerHealthBar.maxValue = PlayerRandInt;
        EnemyHealthBar.maxValue = EnemyRandInt;

        //Set their current health to the max value
        PlayerHealthBar.value = PlayerHealthBar.maxValue;
        EnemyHealthBar.value = EnemyHealthBar.maxValue;

        var RandNumber = Random.Range(0,3);             //Pick a random number between 0 and 3
        enemyElementValue = RandNumber;                 //Set enemy element value to that number
    }

    //For setting the player's element depending on what element is selected
    public void setPlayerElement()
    {
        playerElementValue = elementList.value;
    }
}