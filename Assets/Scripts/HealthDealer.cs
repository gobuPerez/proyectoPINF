using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDealer : MonoBehaviour
{
    // esta funcion destruye al objeto que tiene asignado el script
    public void Hit()
    {

        Destroy(gameObject);
    }

}
