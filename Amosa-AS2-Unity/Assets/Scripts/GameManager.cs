using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject turretPrefab;
    public PlayerController player;
    public float gameTimer;
    public Vector3[] corners;
    public bool isGamePaused;
    public bool[] eventOccurence;
    public int eventTracker;

    void Start() 
    {
        gameTimer = 0f;
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        corners = new Vector3[4];
        corners[0] = new Vector3(-3, 2, 0);
        corners[1] = new Vector3(3, 2, 0);
        corners[2] = new Vector3(3, -2, 0);
        corners[3] = new Vector3(-3, -2, 0);

        eventTracker = 0;
        eventOccurence = new bool[4];
        for (int i = 0; i < eventOccurence.Length; i++)
        {
            eventOccurence[i] = false;
        }
    }

    void Update()
    {
        gameTimer += Time.deltaTime;

        if (player.hitPoints <= 0)
        {
            player.gameObject.SetActive(false);
        }

        if (gameTimer >= 3 && !eventOccurence[0])
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnDrone(1 + i,3);
            }
            eventOccurence[0] = true;
        }
    }

    public void SpawnDrone(int locator, int hitPoints = 1)
    {
        GameObject temp;
        int entryPattern;

        bool kamikazeOn = hitPoints > 1 ? false: true;

        if (locator < 4)
        {
            entryPattern = Random.Range(0, 3);
        }
        else
        {
            entryPattern = Random.Range(3, 6);
        }
        switch (locator)
        {
            case 1:
                temp = Instantiate(dronePrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 2:
                temp = Instantiate(dronePrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 3:
                temp = Instantiate(dronePrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 4:
                temp = Instantiate(dronePrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            default:
                temp = Instantiate(dronePrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;
        }

        var tempC = temp.GetComponent<ShipController>();
        tempC.hitPoints = hitPoints;
        tempC.kamikazeEnabled = kamikazeOn;
        tempC.shipState = ShipController.ShipState.ENTRY;
    }
}
