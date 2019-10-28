using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dronePrefab;
    public PlayerController player;
    public Transform defaultFormation;
    public float gameTimer;

    void Start() 
    {
        gameTimer = 0f;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        gameTimer += Time.deltaTime;

        if (player.hitPoints <= 0)
        {
            player.gameObject.SetActive(false);
        }
    }

    public void SpawnShips(int spawnCount = 1, int index =0)
    {
        int i = 1;
        for (; i <= spawnCount; i++)
        {
            switch (index)
            {
                case 0:
                    break;

                default:
                    break;
            }
        }
    }
}
