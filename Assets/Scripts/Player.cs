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

    Shooter shooter;

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

    // metodo propio https://youtu.be/Zcb8yPEItwA?t=33 
    void Movimiento() {
        
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime; // con deltaTime conseguimos que el movimiento sea independiente de los fps

        Vector2 nuevaPosicion = new Vector2();

        // Nos aseguramos que el jugador no se mueva fuera de la pantalla
        // Clamp asegura que el primer valor dado este en el intervalo formado por los dos siguientes
        // Añadimos paddings para que medio jugador no se salga de la pantalla
        nuevaPosicion.x = Mathf.Clamp(transform.position.x + delta.x, limiteInferior.x + paddingLeft, limiteSuperior.x - paddingRight);
        nuevaPosicion.y = Mathf.Clamp(transform.position.y + delta.y, limiteInferior.y + paddingBottom, limiteSuperior.y - paddingTop);

        transform.position = nuevaPosicion;

    }

    // metodo propio https://youtu.be/Zcb8yPEItwA?t=33 
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

            shooter.isFiring = value.isPressed;

        }


    } 
}
