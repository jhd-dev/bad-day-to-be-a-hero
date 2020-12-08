using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static bool isGameOver;
    [SerializeField] GameObject gameOverBackground;

    private void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            if (!gameOverBackground.activeSelf)
            {
                Time.timeScale = 0;
                gameOverBackground.SetActive(true);
            }
        }
    }
}
