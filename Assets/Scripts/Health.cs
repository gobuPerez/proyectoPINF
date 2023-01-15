using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // el campo SerializeField nos sirve para que la variable aparezca en el inspector de Unity, y pueda ser modificada desde alli
    [SerializeField] int MaxHealth; //Vida maxima del objeto
    int health; // Vida actual del objeto
    [SerializeField] bool isPlayer; //Booleano que indica si el objeto es un jugador o no
    [SerializeField] bool isBuff; //Booleano que indica si el objeto es un buffo o no
    [SerializeField] int score = 50;

    ScoreKeeper scoreKeeper; // objeto que guarda la puntuacion
    LevelManager levelManager; // objeto que se encarga de cambiar de menu
    Shooter shooter;

    // funcion que se ejecuta al comienzo del juego
    void Awake()
    {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        health = MaxHealth; // Al iniciar el objeto, la vida actual es la misma que la maxima

    }

    // funcion que detecta la colision con otro objeto
    void OnTriggerEnter2D(Collider2D other)
    {

        // Si el objeto con el que impactamos no tiene un componente DamageDealer, damageDealer = null;
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        // Si el objeto con el que impactamos no tiene un componente HealthDealer, healthdealer = null;
        HealthDealer healthdealer = other.GetComponent<HealthDealer>();
        shooter = other.GetComponent<Shooter>();

        if (damageDealer != null)
        {

            takeDamage(damageDealer.getDamage());

        }

        if (healthdealer != null)
        {

            //Si el objeto impactado no tiene un health dealer, es un buffo, por lo que da 50 de vida, con la vida maxima como tope
            health += 50;
            if (health > MaxHealth) health = MaxHealth;
        }

    }

    // esta funcion devuelve la salud del objeto que tiene asignado este script
    public int getHealth()
    {

        return health;

    }

    // funcion que reduce la vida del objeto que tiene asignado este script
    void takeDamage(int damage)
    {

        health -= damage;

        // si el objeto se queda sin vida, muere
        if (health <= 0)
        {

            die();

        }

    }

    // esta funcion destruye al objeto que tiene asignado el script. Si el objeto es el jugador, cambia de pantalla (de Game a GameOver)
    void die()
    {

        if (!isPlayer)
        {
            scoreKeeper.modifyScore(score);
            if (shooter != null && !shooter.useAI && isBuff)
                //Si el objeto es un bufo que ha colicionado con el jugador, llamara a PowerUp del componente shooter del jugador antes de morir
                shooter.PowerUp();
        }

        else levelManager.LoadGameOver();

        Destroy(gameObject);

    }

}
