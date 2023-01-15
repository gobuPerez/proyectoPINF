using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // el campo SerializeField nos sirve para que la variable aparezca en el inspector de Unity, y pueda ser modificada desde alli
    [Header("General")] // esto modifica el texto que se muestra en el inspector
    [SerializeField] GameObject projectilePrefab; // objeto proyectil
    [SerializeField] float projectileSpeed = 10f; // velocidad del proyectil. Por defecto, 10
    [SerializeField] float projectileLifetime = 5f; // tiempo de vida del proyectil. Por defecto, 5
    public float baseFiringRate = 0.2f; // tiempo entre disparos. Por defecto, 0.2

    [Header("AI (para enemigos)")] // esto modifica el texto que se muestra en el inspector
    public bool useAI;
    [SerializeField] float firingRateVariance = 0f; // ratio de variacion en la cadencia de disparo
    [SerializeField] float minimumFiringRate = 0.1f; // cadencia de disparo. Por defecto, 0.1

    [HideInInspector] public bool isFiring;
    Player player;  // Referencia al jugador
    FixedJoystick joy; // Referencia al joystck de apuntado del jugador

    Coroutine fireCoroutine;

    // funcion de Unity que se llama cuando el script es usado en el juego
    void Start()
    {
        // si el objeto que tiene el script es un enemigo, dispara de forma automatica
        if (useAI)
        {

            isFiring = true;

        }
        else // Si es el jugador, optiene el script "Player" y su joystick de apuntado
        {
            player = GetComponent<Player>();
            joy = player.joystick2;
        }

    }

    // funcion de Unity que se ejecuta una vez por frame
    void Update()
    {
        // Si hay joystick de apuntado
        if (joy != null)
        {
            isFiring = joy.active(); // dispara si este esta activo
        }
        Fire();
    }

    //Funcion que se encarga de dar mas velocidad a los disparos
    public void PowerUp()
    {
        baseFiringRate *= 0.5f;
    }

    void Fire()
    {

        // al comenzar el juego, fireCroutime es nula
        if (isFiring && fireCoroutine == null)
        {

            fireCoroutine = StartCoroutine(FireContinuously());

        }
        else if (!isFiring && fireCoroutine != null)
        {

            StopCoroutine(fireCoroutine);
            fireCoroutine = null;

        }

    }

    // esta funcion genera los proyectiles que disparan el jugador y los enemigos
    IEnumerator FireContinuously()
    {

        while (true)
        {

            // Instanciamos los proyectiles en el juego
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

            // Obtenemos la componente rigidbody para modificar el movimiento de los proyectiles a nuestra eleccion
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            // Establecemos la direccion y velocidad de los proyectiles disparados
            if (rb != null)
            {

                Vector2 directionProjectile;

                directionProjectile = transform.up;

                rb.velocity = directionProjectile * projectileSpeed;

            }

            Destroy(instance, projectileLifetime); // destruimos el proyectil despues de un tiempo maximo

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance); // tiempo entre disparos

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);

        }

    }


}
