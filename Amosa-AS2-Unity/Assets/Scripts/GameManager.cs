using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;  
    public GameObject m_tempShip;
    public Transform defaultFormation;
    public float gameTimer;


    // Start is called before the first frame update
    void Awake()
    {
        gameTimer = 0f;
    }

    void Start() 
    {
        SpawnShips();
    }

    void FixedUpdate()
    {
        gameTimer += Time.deltaTime;
    }

    public void SpawnShips(int spawnCount = 1, int index =0)
    {
        int i = 1;
        for (; i <= spawnCount; i++)
        {
            switch (index)
            {
                case 0:
                    m_tempShip = Instantiate(shipPrefab, new Vector3(-18, 0, (40 + 2)), Quaternion.Euler(new Vector3(0, 180, 0)));
                    break;

                default:
                    break;
            }
        }
    }
}
