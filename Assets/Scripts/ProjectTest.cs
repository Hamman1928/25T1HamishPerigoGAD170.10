using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For using UI buttons

public class ProjectTest : MonoBehaviour
{
    // Player Values
    public int playerAttackValue = 5;
    public int playerLevel = 1; 
    public int playerEXP = 0;
    public int expThreshold = 50;

    // Enemy Values will go into the spawn enemy class and will create random values
    private int enemyHP;
    private int enemyLevel;
    private string enemyName;

    // This Bool will check if level-up is needed and will prevent attack until L is pressed
    private bool canAttack = true; // Allow attacking by default

    // Start is called before the first frame update
    void Start()
    {
        // Enemy Spawns
        SpawnNewEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // If "L" is pressed, level up the player
        if (Input.GetKeyDown(KeyCode.L) && !canAttack)
        {
            LevelUpPlayer(); // Allow leveling up only if canAttack is false
        }

        // Player starts fighting by pressing space bar, only if they are allowed to attack
        if (Input.GetKeyDown(KeyCode.Space) && canAttack) // Press space bar to Attack
        {
            AttackEnemy();
        }
    }

    // Attack the enemy and deal damage based on player's attack value
    void AttackEnemy()
    {
        // This will tell you how much damage you are inflicting at the current level (25% increase per level)
        float modifiedAttackValue = playerAttackValue * Mathf.Pow(1.25f, playerLevel - 1); // Power function used will return the first number raised to the power of the second number (1.25f^-1) Also used my reference to understand this code
        int damageDealt = Mathf.FloorToInt(modifiedAttackValue); // Convert to int for damage dealt

        // Applying the damage to the enemy's health
        enemyHP -= damageDealt;

        // This Debug Log tells you how much damage you did and to whatever enemy  
        Debug.Log("Player dealt " + damageDealt + " damage to the " + enemyName + ".");

        // This will check if the enemy is dead 
        if (enemyHP <= 0)
        {
            Debug.Log("The " + enemyName + " has been defeated!");

            // Gain experience
            GainEXP(Random.Range(20, 50)); // Random EXP gain between 20 and 50

            // Spawn a new enemy
            SpawnNewEnemy();
        }
    }

    // Gain experience and check if the player levels up
    void GainEXP(int amount)
    {
        playerEXP += amount;
        Debug.Log("Player gained " + amount + " EXP. Total EXP: " + playerEXP);

        // Check if the player has enough EXP to level up
        if (playerEXP >= expThreshold)
        {
            Debug.Log("You levelled up!");
            Debug.Log("Press L to Level Up!");
            canAttack = false; // Disable attacking until 'L' is pressed
        }
    }

    // Level up the player and increase the level-up threshold
    void LevelUpPlayer()
    {
        if (playerLevel <= 5) // if player level is below 5, the if function wont continue to the else statment without passing each threshold 
        {
            playerLevel++; // the second plus represents the addition of 1 extra onto the players level
            playerEXP = 0; // Reset EXP
            expThreshold = Mathf.FloorToInt(expThreshold * 1.5f); // Increase the threshold for the next level
            Debug.Log("Level Up! Player is now level " + playerLevel);
            Debug.Log("Next level requires " + expThreshold + " EXP.");

            // After leveling up, allow the player to attack again
            canAttack = true; // You pressed L and are allowed to attack again
        }
        else // this else statment only unlocks 
        {
            Debug.Log("Congratulations! You have reached level 5!");
            Debug.Log("You finished the game, Thanks for playing!");
        }
    }

    // Spawn a new enemy with a random level, name, and HP
    void SpawnNewEnemy()
    {
        enemyLevel = Random.Range(1, 6); // Enemy level is between 1 and 5, 6 cant be chosen as it is usually cannot randomise to the roof int
        enemyName = GetEnemyNameByLevel(enemyLevel); // enemy name dictated by the enemy level
        enemyHP = enemyLevel * 7; // HP is based on enemies level, level 1 has 7 HP, level 2 has 14 HP etc

        Debug.Log("Oh no... a " + enemyName + " has appeared! It's a level " + enemyLevel + " enemy with " + enemyHP + " HP.");
    }

    // Function to get the enemy name based on the enemy level
    string GetEnemyNameByLevel(int level)
    {
        switch (level) // switch will run the level through the "GetEnemyByLevel" and depending on the case used "Names", it will return the code to spawn enemy class
        {
            case 1:
                return "Slime";
            case 2:
                return "Wolf";
            case 3:
                return "Bear";
            case 4:
                return "Ork";
            case 5:
                return "Goblin Mage";
            default:
                return "Unknown"; // In case the level is not in propper range
        }
    }
}
