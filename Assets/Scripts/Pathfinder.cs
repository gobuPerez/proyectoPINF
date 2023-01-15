using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    EnemySpawner enemyspawner; // objeto que se encarga de generar a los enemigos
    WaveConfigSO waveConfig; // scriptable object 
    List<Transform> waypoints; // Lista de posiciones que forman una oleada
    int waypointIndex = 0; // contador

    // funcion que se ejecuta al comienzo del juego
    void Awake()
    {

        // copiamos el objeto enemyspawner
        enemyspawner = FindObjectOfType<EnemySpawner>();

    }

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {

        waveConfig = enemyspawner.getCurrentWave(); // guardamos una oleada
        waypoints = waveConfig.getWaypoints();  // obtenemos los puntos (posiciones en el juego) que forman la oleada
        transform.position = waypoints[waypointIndex].position; // obtenemos la posicion del primer punto de la oleada
    }

    // funcion de unity que se ejecuta una vez por frame
    void Update()
    {
        FollowPath();
    }

    // esta funcion permite que los enemigos se muevan de un punto a otro, siguiendo de esta manera un camino previamente definido
    // por los programadores. Destruye el objeto una vez que llega al ultimo punto de la ruta
    void FollowPath()
    {

        if (waypointIndex < waypoints.Count)
        {

            Vector3 targetPosition = waypoints[waypointIndex].position; // posicion a la que queremos desplazarnos

            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            // calcula una posicion entre transform.position y targetPosition sin moverse mas de delta
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);

            if (transform.position == targetPosition)
            {

                waypointIndex++; // siguiente punto de la ruta

            }

        }
        else
        {

            // se destruye el objeto del juego
            Destroy(gameObject);

        }

    }
}
