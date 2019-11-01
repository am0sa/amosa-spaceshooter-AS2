using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject turretPrefab;
    public GameObject enemyContainer;
    public GameObject menuOverlay;
    public PlayerController player;
    public float gameTimer;
    public Vector3[] corners;
    public bool isGamePaused;
    public bool[] eventOccurence;
    public int eventTracker;
    public int[] invokeEventCounter;

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
        eventOccurence = new bool[5];

        for (int i = 0; i < eventOccurence.Length; i++)
        {
            eventOccurence[i] = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) /*&& isGamePaused*/)
        {
            // UnpauseGame();
            // Debug.Log("Paused: " + isGamePaused);
            isGamePaused =  (isGamePaused) ? UnpauseGame() : PauseGame();
        }

        if (!isGamePaused)
        {
            gameTimer += Time.deltaTime;

            // if (Input.GetKeyDown(KeyCode.Escape))
            // {
            //     PauseGame();
            // }

            if (player.hitPoints <= 0)
            {
                player.gameObject.SetActive(false);
            }

            if (gameTimer >= 3 && (int)gameTimer%1 == 0 && gameTimer - (int)gameTimer <= 0.01 && !eventOccurence[0])
            {
                SpawnDrone(1);
                SpawnDrone(2, 2);

                if (gameTimer >= 33)
                {
                    eventOccurence[0] = true;
                }
            }
            
            if (gameTimer > 13 && (int)gameTimer%3 == 0 && gameTimer - (int)gameTimer <= 0.01 && !eventOccurence[1])
            {
                SpawnDrone(3);
                SpawnTurret(4, 5);

                if (gameTimer >= 60)
                {
                    SpawnTurret(4, 12);
                    SpawnBossDrone(3);
                    eventOccurence[1] = true;
                }
            }

            if (gameTimer > 63 && (int)gameTimer%6 == 0 && gameTimer - (int)gameTimer <= 0.01 && !eventOccurence[2])
            {
                SpawnTurret(2, 7);

                if (gameTimer >= 88)
                {
                    SpawnTurret(3, 12);
                    SpawnBossDrone(1);
                    eventOccurence[2] = true;
                }
            }

            if (gameTimer > 95 && (int)gameTimer%2 == 0 && gameTimer - (int)gameTimer <= 0.01 && !eventOccurence[2])
            {
                SpawnTurret(2, 15);
                SpawnDrone(4, 4);

                if (gameTimer >= 100)
                {
                    SpawnTurret(3, 12);
                    SpawnBossDrone(1, 2.5f, 125);
                    eventOccurence[2] = true;
                }
            }

            if (gameTimer > 125 && (int)gameTimer%2 == 0 && gameTimer - (int)gameTimer <= 0.01 && !eventOccurence[3])
            {
                SpawnTurret(2, 15);
                SpawnDrone(4, 4);
                SpawnDrone(3, 4);
                SpawnDrone(2, 4);
                SpawnDrone(1, 4);

                if (gameTimer >= 128 && !eventOccurence[4])
                {
                    SpawnBossDrone(1, 3, 85);
                    eventOccurence[4] = true;
                }

                if (gameTimer >= 135)
                {
                    SpawnTurret(3, 12);
                    SpawnBossTurret(1, 2.5f, 85);
                    eventOccurence[3] = true;
                }
            }

            if (gameTimer >= 140)
            {
                if (enemyContainer)
                {
                    
                }
            }

        }


    }

    public bool UnpauseGame()
    {
        Time.timeScale = 1;
        menuOverlay.SetActive(false);
        return false;

    }

    public bool PauseGame()
    {
        Time.timeScale = 0;
        menuOverlay.SetActive(true);
        return true;
        // if (!isGamePaused)
        // {
        //     Time.timeScale = 0;
        //     isGamePaused = true;
        //     menuOverlay.SetActive(true);
        // }
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

        temp.transform.SetParent(enemyContainer.transform);
        var tempC = temp.GetComponent<ShipController>();
        tempC.hitPoints = hitPoints;
        tempC.kamikazeEnabled = kamikazeOn;
        tempC.shipState = ShipController.ShipState.ENTRY;
    }

    public void SpawnTurret(int locator, int hitPoints = 1)
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
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 2:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 3:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 4:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            default:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;
        }
        temp.transform.SetParent(enemyContainer.transform);
        var tempC = temp.GetComponent<TurretController>();
        tempC.hitPoints = hitPoints;
        tempC.shipState = TurretController.ShipState.ENTRY;
    }
    
    public void SpawnBossTurret(int locator, float scaleMod = 2.5f,int hitPoints = 50)
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
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 2:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 3:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            case 4:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;

            default:
                temp = Instantiate(turretPrefab, corners[locator - 1], dronePrefab.transform.rotation);
                break;
        }
        temp.transform.SetParent(enemyContainer.transform);
        temp.transform.localScale = temp.transform.localScale * scaleMod;
        var tempC = temp.GetComponent<TurretController>();
        tempC.hitPoints = hitPoints;
        tempC.shipState = TurretController.ShipState.ENTRY;
        tempC.shipSpeed *= 0.7f;
    }

    public void SpawnBossDrone(int locator, float scaleMod = 2.5f,int hitPoints = 30)
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
        temp.transform.SetParent(enemyContainer.transform);
        temp.transform.localScale = temp.transform.localScale * scaleMod;
        var tempC = temp.GetComponent<ShipController>();
        tempC.hitPoints = hitPoints;
        tempC.shipState = ShipController.ShipState.LINE;
        tempC.shipSpeed *= 0.8f;
        tempC.SHOOT_RESET *= 0.5f;
    }

}
