using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // para usar el paquete Input System instalado

public class Player : MonoBehaviour
{

    Vector2 rawInput;

    [SerializeField] float moveSpeed = 5f;

    // limites de pantalla de los que no podra salirse el jugador
    Vector2 limiteInferior;
    Vector2 limiteSuperior;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] Joystick joystick;
    Shooter shooter;
    float giro;

    void Awake() {
        
        shooter = GetComponent<Shooter>();

    }

    void Start() {

        InicializarLimites();

    }

    void Update()
    {

        Movimiento();

    }

    // metodo propio
    void Movimiento()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 moveV = new Vector3(joystick.Horizontal, joystick.Vertical, 0.0f);
            transform.position += moveV.normalized * moveSpeed * Time.deltaTime;
            giro = Vector2.Angle(new Vector2(0.0f, 1.0f), new Vector2(moveV.x, moveV.y));
            if (joystick.Horizontal >= 0)
                giro = -giro;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, giro);
        }
    }

    // metodo propio
    void InicializarLimites() {

        Camera mainCamera = Camera.main; // Camera.main es la camara principal del juego

        /* El viewport es lo que ve la camara, lo que muestra del juego, y siempre tiene tamaño 1x1
            Para obtener el valor de las coordenadas de la camara en el mundo del juego, usamos el metodo ViewportToWorldPoint 
        */        

        limiteInferior = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        limiteSuperior = mainCamera.ViewportToWorldPoint(new Vector2(1,1));

    }

    // metodo unity
    void OnMove(InputValue value) {
        
        // OnMove solo detecta si pulsamos o levantamos una tecla, por lo que el movimiento continuo tiene que realizarse en el Update
        rawInput = value.Get<Vector2>(); 
        // Debug.Log(rawInput);

    }

    // en InputActions hemos cambiado Fire < Action type < Value (en vez de Button), para que detecte cuando soltamos la tecla
    void OnFire(InputValue value) {

        if (shooter != null) {

            shooter.isFiring = true;

        }


    } 
}
