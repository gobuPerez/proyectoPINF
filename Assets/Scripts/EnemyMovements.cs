using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{

    public float moveSpeed = 3f; // velocidad a la que se mueven los enemigos, por defecto, 3
    private Rigidbody2D rb; // componente rigidBody del objeto al que se le asigna el script
    private GameObject _player; // el jugador
    private Vector2 movement; // vector en dos dimensiones

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // funcion de Unity que se ejecuta antes de comenzar a ejecutarse el juego
    void Awake()
    {
        _player = GameObject.Find("Player"); // copiamos en una variable al objeto jugador
    }

    // funcion de Unity que se ejecuta una vez por frame
    void Update()
    {

        if (_player != null)
        { // si el jugador no ha muerto

            // En cada frame guardamos un vector que apunta al jugador desde la posicion del objeto que tiene asignado el script
            Vector3 direction = _player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90f;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;

        }

    }

    // funcion de Unity que se ejecuta cada cierto tiempo fijo
    private void FixedUpdate()
    {
        MoveEnemy(movement); // movemos al jugador
    }

    void MoveEnemy(Vector2 direction)
    {
        // sumamos a la posicion del objeto que tiene asignado este script, el vector que hemos calculado anteriormente, y que apuntaba
        // hacia el jugador
        // De esta manera conseguimos que los enemigos persigan al jugador
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));

    }

}
