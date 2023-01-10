using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    int health;
    [SerializeField] bool isPlayer;
    [SerializeField] bool isBuff;
    [SerializeField] int score = 50;
    
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Shooter shooter;

    void Awake() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        health = MaxHealth;

    }

    void OnTriggerEnter2D(Collider2D other) {
        
        // Si el objeto con el que impactamos no tiene un componente DamageDealer, damageDealer = null;
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        HealthDealer healthdealer = other.GetComponent<HealthDealer>();
        shooter = other.GetComponent<Shooter>();

        if (damageDealer != null) {
            
            takeDamage(damageDealer.getDamage());
            Debug.Log("1");

        } 

        if (healthdealer != null) {

            health += 50;
            if (health > MaxHealth)
                health = MaxHealth;
        }
    
    }

    public int getHealth() {

        return health;

    }

    void takeDamage (int damage) {

        health -= damage;
        
        if (health <= 0) {

            die();

        }

    }

    void die() {

        if (!isPlayer)
        { scoreKeeper.modifyScore(score);
            if(shooter != null && !shooter.useAI && isBuff)
            shooter.PowerUp();
        }

        else
            levelManager.LoadGameOver();

        Destroy(gameObject);

    }
}
