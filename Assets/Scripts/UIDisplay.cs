using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    // el campo SerializeField nos sirve para que la variable aparezca en el inspector de Unity, y pueda ser modificada desde alli
    [Header("Health")] // esto modifica el texto que se muestra en el inspector
    [SerializeField] Slider healthSlider; // barra de vida del jugador
    [SerializeField] Health playerHealth; // salud del jugador

    [Header("Score")] // esto modifica el texto que se muestra en el inspector
    [SerializeField] TextMeshProUGUI scoreText; // elemento de la interfaz que va a mostrar la puntuacion del jugador
    ScoreKeeper scoreKeeper; // puntuacion del jugador

    // funcion de Unity que se ejecuta al comienzo del juego
    void Awake()
    {

        // copiamos el objeto puntuacion
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {
        // hacemos que el valor maximo de la barra de vida que se muestra en pantalla sea igual a la salud maxima del jugador al comienzo del juego
        healthSlider.maxValue = playerHealth.getHealth();
    }

    // funcion de Unity que se ejecuta una vez por frame
    void Update()
    {
        healthSlider.value = playerHealth.getHealth(); // se actualiza el valor de la barra de vida conforme la salud del jugador cambia
        scoreText.text = scoreKeeper.getScore().ToString("000000000"); // actulizamos la puntuacion del jugador que se muestra en pantalla conforme esta va variando
    }
}
