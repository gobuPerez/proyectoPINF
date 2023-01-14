using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int MaxHealth; //Vida máxima del objeto
    int health; //Vida actual del objeto
    [SerializeField] bool isPlayer; //Booleano que indica si el objeto es un jugador o no
    [SerializeField] bool isBuff; //Booleano que indica si el objeto es un buffo o no
    [SerializeField] int score = 50;
    
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Shooter shooter;

    void Awake() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        health = MaxHealth; //Al iniciar el objeto, la vida actual es la misma que la maxima

    }

    void OnTriggerEnter2D(Collider2D other) {
        
        // Si el objeto con el que impactamos no tiene un componente DamageDealer, damageDealer = null;
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        HealthDealer healthdealer = other.GetComponent<HealthDealer>();
        shooter = other.GetComponent<Shooter>();

        if (damageDealer != null) {
            
            takeDamage(damageDealer.getDamage());

        } 

        if (healthdealer != null) {

            //Si el objeto impactado no tiene un health dealer, es un buffo, por lo que da 50 de vida, con la vida maxima como tope
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
                //Si el objeto es un bufo que ha colicionado con el jugador, llamará a PowerUp del componente shooter del jugador antes de morir
            shooter.PowerUp();
        }

        else
            levelManager.LoadGameOver();

        Destroy(gameObject);

    }
}
