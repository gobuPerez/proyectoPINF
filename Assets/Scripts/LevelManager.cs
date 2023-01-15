using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para cargar las distintas escenas del videojuego

// En build settings hay que añadir las tres escenas del videojuego e indicar el orden en el que queremos que se rendericen
public class LevelManager : MonoBehaviour
{

    [SerializeField] float delay = 2f; // tiempo de retardo entre una pantalla y otra

    ScoreKeeper scoreKeeper;

    // funcion de unity que se ejecuta al comienzo del juego
    void Awake()
    {

        // copiamos el objeto puntuacion
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    // LAS SIGUIENTES FUNCIONES SIRVEN PARA PASAR DE UN PANTALLA DE JUEGO A OTRA

    public void LoadGame()
    {

        scoreKeeper.resetScore(); // ponemos la puntuacion a cero
        // cargamos la escena que queremos a partir del nombre. Tambien puede hacerse usando el indice de la escena en build settings
        SceneManager.LoadScene("Game"); // cambio de menu (a la pantalla de juego)

    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu"); // cambio de menu (al menu principal)

    }

    public void LoadGameOver()
    {

        StartCoroutine(WaitAndLoad("GameOver", delay)); // cambio de menu (a la pantalla de game over). Se aplica un tiempo de retardo

    }

    public void QuitGame()
    {

        Application.Quit();

    }

    // esta funcion aplica un pequeño retraso a la carga entre pantallas
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }

}
