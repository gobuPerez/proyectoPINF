using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPTABLE OBJECT
[CreateAssetMenu(fileName = "WaveConfig", menuName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{

    [SerializeField] List<GameObject> enemysPrefabs; // lista de enemigos que van a ir instanciandose durante la partida
    [SerializeField] List<GameObject> buffPrefabs; // lista de buffos que van a ir instanciandose durante la partida
    [SerializeField] Transform pathPrefab; // posicion
    [SerializeField] float moveSpeed = 5f; // velocidad de movimiento
    [SerializeField] float timeBetweenEnemySpawns = 1f;  // tiempo entre oleadas de enemigos
    [SerializeField] float spawnTimeVariance = 0f; // variacion que usamos para generar un numero aleatorio. Si es 0, todos los enemigos se generan en tiempos iguales
    [SerializeField] float minimunSpawnTime = 0.2f; // tiempo minimo entre oleadas

    // NECESITAMOS METODOS OBSERVADORES PORQUE DENTRO DE UN SCRIPTABLEOBJECT LOS ATRIBUTOS SON PRIVADOS

    // funcion que devuelve el numero de enemigos para instanciar
    public int getEnemyCount()
    {

        return enemysPrefabs.Count;
    }

    // funcion que devuelve el numero de buffos para instanciar
    public int getBuffCount()
    {

        return buffPrefabs.Count;
    }

    // funcion que devuelve un enemigo concreto de la lista de enemigos
    public GameObject getEnemyPrefab(int index)
    {

        return enemysPrefabs[index];

    }

    // fincion que devuelve un buffo concreto de la lista de buffos
    public GameObject getBuffPrefab(int index)
    {

        return buffPrefabs[index];
    }

    // funcion que devuelve la posicion del primer punto de la ruta que recibe el script
    public Transform getStartingWaypoint()
    {

        return pathPrefab.GetChild(0); // primer hijo del path, es decir, primer punto de la ruta

    }

    // funcion que devuelve una lista de puntos que forman una ruta
    public List<Transform> getWaypoints()
    {

        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab)
        {

            waypoints.Add(child);
        }

        return waypoints;
    }

    // funcion que devuelve la velocidad de movimiento del objeto que tiene asignado este script
    public float GetMoveSpeed()
    {

        return moveSpeed;

    }

    // esta funcion calcula y devuelve un tiempo aleatorio entre unos limites dados
    public float getRandomSpawnTime()
    {

        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimunSpawnTime, float.MaxValue); // float.MaxValue: valor maximo de float
    }

}