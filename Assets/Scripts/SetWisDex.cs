using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWisDex : MonoBehaviour
{
    public int minStat;                    //Minimum value any of these stats can be
    public int maxStat;                    //Maximum value any of these stats can be
    public int playerDexterity;
    public int enemyDexterity;
    public int playerWisdom;
    public int enemyWisdom;

    void Start()
    {
        playerDexterity = Random.Range(minStat,maxStat);             //Set player dexterity to a random value between these values
        enemyDexterity = Random.Range(minStat,maxStat);             //Set enemy dexterity to a random value between these values
        playerWisdom = Random.Range(minStat,maxStat);             //Set player wisdom to a random value between these values
        enemyWisdom = Random.Range(minStat,maxStat);             //Set enemy wisdom to a random value between these values
    }
}