using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSetup : MonoBehaviour
{
    public Slider PlayerHealthBar;
    public Slider EnemyHealthBar;
    public int MinHealth;                   //Having these makes it easier to change the min/max values while in unity editor
    public int MaxHealth;

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
    }
}