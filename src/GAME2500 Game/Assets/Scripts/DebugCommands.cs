using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCommands : MonoBehaviour
{
    [SerializeField] KeyCode resetLevel;
    [SerializeField] KeyCode exitGame;

    void Update()
    {
        if (Input.GetKeyDown(resetLevel))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(exitGame))
        {
            Application.Quit();
        }
    }

    void ReloadScene()
    {
        ControlCenter.inCameraMode = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
