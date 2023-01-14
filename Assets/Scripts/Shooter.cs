using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")] // esto modifica el texto que se muestra en el inspector
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    public float baseFiringRate = 0.2f; // tiempo entre disparos

    [Header("AI (para enemigos)")]
    public bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;
    Player player;  //Referencia al jugador
    FixedJoystick joy; //Referencia al joystck de apuntado del jugador

    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // si el objeto que tiene el script es un enemigo, dispara de forma automatica
        if (useAI)
        {

            isFiring = true;

        }
        else ///Si es un jugado, optiene el script "Player" y su joystick de apuntado
        { 
            player = GetComponent<Player>();
            joy = player.joystick2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Si hay joystick de apuntado
        if (joy != null)
        {
            isFiring = joy.active(); //Dispara si este está activo
        }
        Fire();   
    }

    //Función que se encarga de dar mas velocidad a los disparos
    public void PowerUp()
    {
        baseFiringRate *= 0.5f;
    }

    void Fire() {
        
        // al comenzar el juego, fireCroutime es nula
        if (isFiring && fireCoroutine == null) {
    
            fireCoroutine = StartCoroutine(FireContinuously());

        } else if (!isFiring && fireCoroutine != null) {

            StopCoroutine(fireCoroutine);
            fireCoroutine = null;

        }

    }

    IEnumerator FireContinuously() {

        while (true) {

            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null) {

                Vector2 directionProjectile;

                directionProjectile = transform.up;

                rb.velocity = directionProjectile * projectileSpeed;

            }

            Destroy(instance, projectileLifetime); // destruimos el proyectil despues de un tiempo maximo

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);

        }

    }


}
