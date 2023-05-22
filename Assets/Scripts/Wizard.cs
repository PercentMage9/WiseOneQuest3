using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Yup, its the wizard class, woo-ey
//Basically just has all the wizard stat stuff so it makes wizard creation easier n stuff
public class Wizard
{
    public string name;
    public Elements element;
    public int dexterity;
    public int wisdom;
    public int currentHealh;
    public int maxHealth;
}
