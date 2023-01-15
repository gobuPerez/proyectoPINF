using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // para usar el paquete Input System instalado

public class Player : MonoBehaviour
{

    Vector2 rawInput; // input de los controles

    [SerializeField] float moveSpeed = 5f; // velocidad del jugador

    [SerializeField] FixedJoystick joystick1; // Joystick de movimiento
    public FixedJoystick joystick2; //Joystick de apuntado. Debe ser publico para que Shooter pueda acceder a él
    Vector3 moveV; // Vector3 para el movimieento
    Shooter shooter;
    Vector2 arriba; // Vector2 que apunta hacia arriba
    Vector2 vgiro; // Vector2 de giro
    float giro; // Angulo de giro del personaje

    // funcion de Unity que se ejecuta al comienzo del juego
    void Awake()
    {

        shooter = GetComponent<Shooter>();
        arriba.x = 0f;
        arriba.y = 1f;

    }

    // funcion de Unity que se ejecuta una vez por frame
    void Update()
    {

        Movimiento();

    }

    void Movimiento()
    {
        if (joystick1.active()) //Si el joystick de movimiento está en uso
        {

            moveV = new Vector3(joystick1.Horizontal, joystick1.Vertical, 0.0f);  // Se obtiene la dirección del movimiento

            // Se añade a la poscíón el vector normalizado multiplicado por la velocidad, con deltaTime conseguimos que el movimiento sea independiente de los fps
            transform.position += moveV.normalized * moveSpeed * Time.deltaTime;


        }
        if (joystick2.active()) //Si el joystick de apuntado está en uso
        {
            vgiro.x = joystick2.Horizontal;
            vgiro.y = joystick2.Vertical;
            giro = Vector2.Angle(arriba, vgiro); // Se obtiene el angulo entre el vector correspondiente al joystick de apuntado y la eje y

            if (joystick2.Horizontal >= 0) giro = -giro; //Si el vector de giro está a la derecha, el ángulo es negativo

            transform.eulerAngles = new Vector3(0.0f, 0.0f, giro); //Se aplica el angulo
        }


    }


    // metodo de Unity
    void OnMove(InputValue value)
    {

        // OnMove solo detecta si pulsamos o levantamos una tecla, por lo que el movimiento continuo tiene que realizarse en el Update
        rawInput = value.Get<Vector2>();

    }

    // en InputActions hemos cambiado Fire < Action type < Value (en vez de Button), para que detecte cuando soltamos la tecla
    void OnFire(InputValue value)
    {

        if (shooter != null)
        {

            shooter.isFiring = true;

        }

    }
}

