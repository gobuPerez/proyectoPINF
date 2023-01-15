using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;

    static ScoreKeeper instance; // objeto estatico puntuacion

    // funcion de Unity que se ejecuta al comienzo del juego
    void Awake()
    {

        // En Unity, un singleton es un objeto que puede ser compartido por varias escenas distintas
        // En nuestro caso, la puntuacion tiene que mantenerse entre la pantalla de juego y la de game over, que son dos entornos independientes
        ManageSingleton();

    }

    void ManageSingleton()
    {

        if (instance != null)
        { // Si ya existe un objeto puntuacion en el juego, destruimos el nuevo objeto creado por este script

            gameObject.SetActive(false);
            Destroy(gameObject);

        }
        else
        { // Si no existe un objeto puntuacion en el juego, el objeto creado por este script es el nuevo objeto puntuacion

            instance = this;
            DontDestroyOnLoad(gameObject);

        }

    }

    // esta funcion devuelve la puntuacion
    public int getScore()
    {

        return score;

    }

    // esta funcion modifica la puntuacion
    public void modifyScore(int value)
    {

        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);

    }

    // esta funcion resetea la puntuacion
    public void resetScore()
    {

        score = 0;

    }
}
