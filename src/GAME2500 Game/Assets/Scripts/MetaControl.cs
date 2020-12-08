using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaControl : MonoBehaviour
{
    public static bool mouseIsShoot = false;

    public void ToggleMouseShoot()
    {
        mouseIsShoot = !mouseIsShoot;
    }
}
