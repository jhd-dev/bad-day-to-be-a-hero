using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCenter : MonoBehaviour
{
    public static bool inCameraMode = true;
    [SerializeField] Text modeText; 

    void Update()
    {
        if (inCameraMode)
        {
            modeText.text = "Camera Mode";
        }
        else
        {
            modeText.text = "Minion Mode";
        }
    }
}
