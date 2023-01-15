using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10; // daño que hace el objeto que tiene asignado este script. Por defecto, 10 puntos

    // esta funcion devuelve el daño que hace un objeto que tiene asignado este script
    public int getDamage()
    {

        return damage;

    }

    // esta funcion destruye el objeto (lo elimina de la pantalla de juego) que tiene asignado este script
    public void Hit()
    {

        Destroy(gameObject);

    }

}
