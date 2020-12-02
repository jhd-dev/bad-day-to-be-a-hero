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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(exitGame))
        {
            Application.Quit();
        }
    }
}
