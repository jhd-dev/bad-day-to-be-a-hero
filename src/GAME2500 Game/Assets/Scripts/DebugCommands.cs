using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour
{
    [SerializeField] KeyCode resetLevel;
    
    void Update()
    {
        if (Input.GetKeyDown(resetLevel))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        ControlCenter.inCameraMode = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
