using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCenter : MonoBehaviour
{
    public static bool inCameraMode = false;
    [SerializeField] Text modeText; 

    void Update()
    {
        if (inCameraMode)
        {
            modeText.text = "Camera Mode";
        }
        else
        {
            modeText.text = "Villain Mode";
        }
    }
}
