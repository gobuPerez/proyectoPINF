using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para cargar las distintas escenas del videojuego


// En build settings hay que añadir las tres escenas del videojuego e indicar el orden en el que queremos que se rendericen
public class LevelManager : MonoBehaviour
{

    [SerializeField] float delay = 2f; 

    ScoreKeeper scoreKeeper;

    void Awake () {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    public void LoadGame () {

        scoreKeeper.resetScore();
        // cargamos la escena que queremos a partir del nombre. Tambien puede hacerse usando el indice de la escena en build settings
        SceneManager.LoadScene("Game");

    }

    public void LoadMainMenu () {
        
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadGameOver () {

        StartCoroutine(WaitAndLoad("GameOver", delay));

    }

    public void QuitGame () {
        // puede que no funcione bien en moviles
        Debug.Log("Saliendo...");
        Application.Quit();

    }

    // Para aplicar un pequeño retraso a la carga de la pantalla de game over
    IEnumerator WaitAndLoad (string sceneName, float delay) {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }

}
