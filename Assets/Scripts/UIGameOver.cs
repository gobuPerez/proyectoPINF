using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{

    // el campo SerializeField nos sirve para que la variable aparezca en el inspector de Unity, y pueda ser modificada desde alli
    [SerializeField] TextMeshProUGUI scoreText; // elemento de la interfaz que va a mostrar la puntuacion del jugador
    ScoreKeeper scoreKeeper; // puntuacion del jugador

    // funcion de Unity que se ejecuta al comienzo del juego
    void Awake()
    {

        // guardamos la puntuacion del jugador en una variable
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {

        // actulizamos la puntuacion del jugador que se muestra en la pantalla de game over
        scoreText.text = scoreKeeper.getScore() + " puntos!";

    }

}
