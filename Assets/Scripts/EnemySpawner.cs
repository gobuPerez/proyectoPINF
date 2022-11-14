using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping = true;

    void Start() {

        // Tenemos que llamar al metodo de esta manera porque es una corutina
        StartCoroutine(spawnEnemiesWaves());

    }

    public WaveConfigSO getCurrentWave () {

        return currentWave;
        
    }    

    IEnumerator spawnEnemiesWaves () {

        do {

            for (int i = 0; i < waveConfigs.Count; i++) {

                currentWave = waveConfigs[i];
            
                // Quaternion.identity indica que el objeto no tiene rotacion
                for (int j = 0; j < currentWave.getEnemyCount(); j++)  {
                    
                    Instantiate(currentWave.getEnemyPrefab(j), currentWave.getStartingWaypoint().position, Quaternion.identity, transform);

                    yield return new WaitForSeconds(currentWave.getRandomSpawnTime());

                }

                yield return new WaitForSeconds(timeBetweenWaves);

            }

        } while(isLooping);

    }

}
