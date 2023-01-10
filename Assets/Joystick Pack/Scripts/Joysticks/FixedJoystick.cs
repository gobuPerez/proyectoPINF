using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public bool active()
    {
        if (Horizontal == 0 && Vertical == 0)
            return true;
        else
            return false;
    }
}