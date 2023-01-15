using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{

    // el campo SerializeField nos sirve para que la variable aparezca en el inspector de Unity, y pueda ser modificada desde alli
    [SerializeField] List<WaveConfigSO> waveConfigs; // Lista de oleadas que recibe el script
    [SerializeField] float timeBetweenWaves = 0f; // tiempo de espera entre oleadas de buffos. Por defecto, 0
    WaveConfigSO currentWave; // scriptableObject
    [SerializeField] bool isLooping = true;

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {

        // llamada a la corutina que inicia las oleadas de buffos
        StartCoroutine(spawnBuffWaves());

    }

    // esta funcion devuelve la oleada de buffos actual
    public WaveConfigSO getCurrentWave()
    {

        return currentWave;

    }

    // esta funcion selecciona una de las oleadas de buffos de la lista que recibe el script y la instancia en el juego
    IEnumerator spawnBuffWaves()
    {

        do
        {

            for (int i = 0; i < waveConfigs.Count; i++)
            { // recorremos toda la lista de oleadas

                currentWave = waveConfigs[i]; // una oleada de la lista

                for (int j = 0; j < currentWave.getBuffCount(); j++)
                { // recorremos los buffos que tiene la oleada

                    // instanciamos un buffo en el juego
                    Instantiate(currentWave.getBuffPrefab(j), currentWave.getStartingWaypoint().position, transform.rotation);

                    yield return new WaitForSeconds(currentWave.getRandomSpawnTime()); // tiempo aleatorio entre buffos
                }

                yield return new WaitForSeconds(timeBetweenWaves); // tiempo aleatorio entre oleadas

            }

        } while (isLooping);
    }
}