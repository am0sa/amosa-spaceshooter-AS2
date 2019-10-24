using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject enemyPath;
    public GameObject m_tempShip;
    public Transform defaultFormation;
    public List<ShipController> shipController;
    public List<Vector3> FlightPattern; //points for ship attack patterns
    public List<GameObject> enemies;
    public int currentWaypoint;
    public int shipIndex;
    public float gameTimer;
    private int randomIntMax3;

    // Start is called before the first frame update
    void Awake()
    {
        gameTimer = 0f;
        shipIndex = 0;

        FlightPattern = new List<Vector3>();
        FlightPattern.Add(new Vector3 (-10, 0, -5));
        FlightPattern.Add(new Vector3 (0, 0, -5));
        FlightPattern.Add(new Vector3 (10, 0, -5));
        FlightPattern.Add(new Vector3 (-15, 0, -10));
        FlightPattern.Add(new Vector3 (-5, 0, -10));
        FlightPattern.Add(new Vector3 (5, 0, -10));
        FlightPattern.Add(new Vector3 (15, 0, -10));
        FlightPattern.Add(new Vector3 (-10, 0, -15));
        FlightPattern.Add(new Vector3 (0, 0, -15));
        FlightPattern.Add(new Vector3 (10, 0, -15));
        FlightPattern.Add(new Vector3 (-15, 0, -20));
        FlightPattern.Add(new Vector3 (-5, 0, -20));
        FlightPattern.Add(new Vector3 (5, 0, -20));
        FlightPattern.Add(new Vector3 (15, 0, -20));

        shipController = new List<ShipController>();
    }

    void Start() 
    {
        SpawnShips();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTimer += Time.deltaTime;
        randomIntMax3 = Random.Range(0,3);
        if (((int)gameTimer % 10) == 0 && gameTimer < ((int)gameTimer + Time.deltaTime))
        {
            SpawnShips(1, randomIntMax3);
            Debug.Log("New Enemy");
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
                    m_tempShip = Instantiate(shipPrefab, new Vector3(-18, 0, (40 + 2)), Quaternion.Euler(new Vector3(0, 180, 0)));
                    break;

                case 1:
                    m_tempShip = Instantiate(shipPrefab, new Vector3(-9, 0, (40 + 2)), Quaternion.Euler(new Vector3(0, 180, 0)));
                    break;

                case 2:
                    m_tempShip = Instantiate(shipPrefab, new Vector3(-9, 0, (40 + 2)), Quaternion.Euler(new Vector3(0, 180, 0)));
                    break;

                case 3:
                    m_tempShip = Instantiate(shipPrefab, new Vector3(-18, 0, (40 + 2)), Quaternion.Euler(new Vector3(0, 180, 0)));
                    break;

                default:
                    break;
            }
            shipIndex+=i;
            m_tempShip.transform.name = "ship" + shipIndex;
            shipController.Add(m_tempShip.GetComponent<ShipController>());
        }
    }

    public List<Vector3> GetFlightPattern()
    {
        return FlightPattern;
    }
}

/*
    The Entire game is on a grid with 40W x 80H
    X-Pos = Width
    Z-Pos = Height
    The midpoint is 20W x 40H
 */