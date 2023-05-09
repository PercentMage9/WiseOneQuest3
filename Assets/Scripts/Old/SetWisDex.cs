using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWisDex : MonoBehaviour
{
    public int minWisDexStat;                    //Minimum value any of these stats can be
    public int maxWisDexStat;                    //Maximum value any of these stats can be
    public int playerDexterity;
    public int enemyDexterity;
    public int playerWisdom;
    public int enemyWisdom;

    void Start()
    {
        playerDexterity = Random.Range(minWisDexStat,maxWisDexStat);             //Set player dexterity to a random value between these values
        enemyDexterity = Random.Range(minWisDexStat,maxWisDexStat);             //Set enemy dexterity to a random value between these values
        playerWisdom = Random.Range(minWisDexStat,maxWisDexStat);             //Set player wisdom to a random value between these values
        enemyWisdom = Random.Range(minWisDexStat,maxWisDexStat);             //Set enemy wisdom to a random value between these values
    }
}