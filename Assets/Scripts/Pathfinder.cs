using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    EnemySpawner enemyspawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake() {

        enemyspawner = FindObjectOfType<EnemySpawner>();

    }

    // Start is called before the first frame update
    void Start()
    {
        waveConfig = enemyspawner.getCurrentWave();
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();        
    }

    void FollowPath () {

        if (waypointIndex < waypoints.Count) {

            Vector3 targetPosition = waypoints[waypointIndex].position;

            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            
            // calcula una posicion entre transform.position y targetPosition sin moverse mas de delta
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);

            if (transform.position == targetPosition) {

                waypointIndex++;

            }

        } else {

            Destroy(gameObject);

        }

    }
}
