using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject {
    
    // lista de enemigos que van a ir instanciandose durante la partida
    [SerializeField] List<GameObject> enemysPrefabs;
    [SerializeField] List<GameObject> buffPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f; // 
    [SerializeField] float spawnTimeVariance = 0f; // variacion que usamos para generar un numero aleatorio. Si es 0, todos los enemigos se generan en tiempos iguales
    [SerializeField] float minimunSpawnTime = 0.2f;

    // necesitamos metodos observadores porque dentro de un ScriptableObject los atributos son privados

    public int getEnemyCount () {

        return enemysPrefabs.Count;
    }
    public int getBuffCount () {

        return buffPrefabs.Count;
    }

    public GameObject getEnemyPrefab (int index) {

        return enemysPrefabs[index];

    }
    public GameObject getBuffPrefab (int index) {

        return buffPrefabs[index];
    }

    public Transform getStartingWaypoint () {

        return pathPrefab.GetChild(0); // primer hijo del path, es decir, primer punto de la ruta

    }

    public List<Transform> getWaypoints () {

        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab) {
            
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed() { 
        
        return moveSpeed; 
    
    }

    public float getRandomSpawnTime () {

        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimunSpawnTime, float.MaxValue); // float.MaxValue: valor maximo de float
    }

}