using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    void OnTriggerEnter2D(Collider2D other) {
        
        // Si el objeto con el que impactamos no tiene un componente DamageDealer, damageDealer = null;
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        HealthDealer healthdealer = other.GetComponent<HealthDealer>();

        if (damageDealer != null) {
            
            takeDamage(damageDealer.getDamage());

        } 

        if (healthdealer != null) {

            health = 50;
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

        if (!isPlayer) {

            scoreKeeper.modifyScore(score);

        } else {

            levelManager.LoadGameOver();

        }

        Destroy(gameObject);

    }
}
