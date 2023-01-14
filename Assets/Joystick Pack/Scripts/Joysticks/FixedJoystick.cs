using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{

    //Funcion que indica si el joystick está o no en uso
    public bool active()
    {
        if (Horizontal == 0 && Vertical == 0)
            return false;
        else
            return true;
    }
}