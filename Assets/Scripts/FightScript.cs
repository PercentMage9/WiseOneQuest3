//Theres way too much annotation on here
//I never really annotate unless its for group projects
//Even then my annotation is like "this works, keep it"
//Everything is all over the place and isnt
//in places that make sense but it works
//good luck.
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//"Ethan's stupid enums" - Ava
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

    //Game objects n stuff
    public GameObject fightPanel;
    public GameObject screens;
    public GameObject winnerScreen;

    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public TextMeshProUGUI button3Text;
    public TextMeshProUGUI button4Text;
    public TextMeshProUGUI winnerText;

    //Attack names array for assigning to button text, I hate how I did this but whatever
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

    //Element setup thing
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

    //Boolean used for if its the players turn or not
    private bool isItPlayersTurn;

    //Initiate wizardification! yar!
    public void WizardInitialize(Wizard wizard,Slider healthBar)
    {

        //Element setup
        var RandNumber = Random.Range(0, 3);
        wizard.element = (Elements)RandNumber;

        //Wisdom dexterity setup
        wizard.dexterity = Random.Range(minWisDexStat, maxWisDexStat);
        wizard.wisdom = Random.Range(minWisDexStat, maxWisDexStat);

        //Health setup
        int healthRandInt = Mathf.RoundToInt(Random.Range(minHealth, maxHealth) / 10) * 10;

        //Set their current health to the max value
        wizard.maxHealth = healthRandInt;
        wizard.currentHealh = wizard.maxHealth;
    }

    //On start, does stuff when it starts but not really since it does some stuff before this anyway, useless!!
    void Start()
    {
        playerWizard = new Wizard();
        enemyWizard = new Wizard();

        WizardInitialize(playerWizard, playerHealthBar);
        WizardInitialize(enemyWizard, enemyHealthBar);
    }

    //For setting the player's element depending on what element is selected, I think, I sort of forgot and am running out of time, whatever it is this thing is important for setting player elements n stuff
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

    //Does stuff every frame or whenever it feels like it sometimes because just like how inconsistent Start() is, its bad and lies about what it does but whatever it works
    public void Update()
    {
        //Sets health bar values to the current wizard health values so that the health bars are accurate to the current healths
        playerHealthBar.maxValue = playerWizard.maxHealth;
        enemyHealthBar.maxValue = enemyWizard.maxHealth;

        playerHealthBar.value = playerWizard.currentHealh;
        enemyHealthBar.value = enemyWizard.currentHealh;
        //This is a really cheap and inefficient way of doing it since
        //it doesnt really need to be re-set each update, but it works
        //and doesnt take up a significant amount of resources and so
        //I dont wanna risk breaking it by moving it to someplace else
    }

    //For starting the game
    public void StartGame()
    {
        //Start the fight!! checks the dexterity to see who should attack first on the first round
        //we dont need to do this again since attacks take turns on from this, and so its only ever called once
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

    //So this is where things take a turn for the un-organised worse, good luck

    //So, really hard to explain why this is here and why it fixed my problem, but it did. Basically I put this in
    //so that I can check if its the player's turn, and either show/hide the fight panel and pass in the chosen attacks etc
    //I think I did this because of the fact that I couldn't pass in more than one value into the WizardAttack method, this is
    //a really whacky workaround, I think, I sort of forgot, just dont touch this ok thank you
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

    //This is really poorly named, basically this checks for if any wizard is dead and then will call the gameover method with either
    //true or false passed in, gameover true meaning the player won and false meaning the player lost, again really a cheap way to do it
    //and then also it does stuff with the fight panel for if it is/isnt the players turn
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

    //Basically either recieves true or false from another method somewhere else in this unorganised mess and then will declare the winner depending on that
    public void GameOver(bool hasPlayerWon)
    {
        screens.SetActive(false);
        //I really like this thing for quick and dirty true/false comparing for things like this, so nice and readable
        winnerText.text = (hasPlayerWon ? playerWizard.name : enemyWizard.name) + " has won!";
        winnerScreen.SetActive(true);
    }

    //You really want me to annotate this? I never even annotate for personal things anyway, only group projects (this isnt a group project)
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    //This is for like determening damage to deal and to who and stuff, just read it and you'll see what it does, fairly readable code
    public IEnumerator WizardAttack(Wizard attackingWizard,Wizard defendingWizard,int chosenAttack)
    {
        fightPanel.SetActive(false);        //Close the fight panel, done to make sure its closed (cause it was randomly not closing, idk why)
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
        else        //Fallback for incase the chosen attack somehow equals something else, once had that happen but I dont think this version does that any more
        {
            damageToDeal = (attackingWizard.dexterity + 20);
        }

        //Multiply damage depending on element type
        //Enums my beloved, its nice and readable and not
        //gobbledigoop numbers n stuff
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

        //The bit that actually hurts them
        defendingWizard.currentHealh = (defendingWizard.currentHealh - damageToDeal);
        isItPlayersTurn = !isItPlayersTurn;
        StartWizardTurn();
        Debug.Log("Turn ended!");
    }

    //What do you think it does, used for when the attacking wizard has an elemental advantage
    public void MultiplyDamage()
    {
        damageToDeal = (int)(damageToDeal * 1.5f);
    }
}
