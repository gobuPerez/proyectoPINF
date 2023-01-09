using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping = true;

    void Start() {

        // Tenemos que llamar al metodo de esta manera porque es una corutina
        StartCoroutine(spawnBuffWaves());

    }

    public WaveConfigSO getCurrentWave () {

        return currentWave;
        
    }    

    IEnumerator spawnBuffWaves () {

        do {

            for (int i = 0; i < waveConfigs.Count; i++) {

                currentWave = waveConfigs[i];
            
                // Quaternion.identity indica que el objeto no tiene rotacion
                for (int j = 0; j < currentWave.getBuffCount(); j++)  {
                    
                    Instantiate(currentWave.getBuffPrefab(j), currentWave.getStartingWaypoint().position, transform.rotation);

                    yield return new WaitForSeconds(currentWave.getRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while(isLooping);
    }
}