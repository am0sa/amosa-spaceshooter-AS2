using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameManager gameManager;

    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.SetActive(false);
    }

    public void UnpauseGame()
    {
        gameManager.isGamePaused = gameManager.UnpauseGame();
    }

    public void RestartGame()
    {
        gameManager.ClearAll();
        gameManager.isGamePaused = gameManager.UnpauseGame();
    }
}
