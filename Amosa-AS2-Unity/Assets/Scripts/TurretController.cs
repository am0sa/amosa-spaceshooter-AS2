using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public enum ShipState
    {
        ENTRY, //Move through entry waypoints
        LINE, //Patrol a line
        LURK,  //Stationary at a point
    }
    public ShipState shipState; 
    private GameManager gameManager;
    public GameObject enemyBulletPrefab;
    public GameObject hub;
    private Formation formation;

    public bool kamikazeEnabled;

    public float shipSpeed;
    public float DEFAULT_SHIP_SPEED;
    private float lurkTimer;
    private float DEFAULT_LURK_TIME;
    public float bulletForce;
    public float shootTimer;
    public float SHOOT_RESET;

    public Rigidbody2D rigidBody;
    public GameObject player;
    public GameObject[] node;

    public List<int[]> entryPattern;
    public int[] entryPattern1;
    public int[] entryPattern2;
    public int[] entryPattern3;
    public int[] entryPattern4;
    public int[] entryPattern5;
    public int[] entryPattern6;
    public int nodeTracker;
    public int entryIndex;



    private void Awake()
    {
        if (tag != "Drone")
        {
            kamikazeEnabled = false;
        }
        else
        {
            kamikazeEnabled = true;
        }

        entryPattern1 = new int[] { 10, 7, 9, 11, 10 };
        entryPattern2 = new int[] { 11, 6, 4, 1, 2 };
        entryPattern3 = new int[] { 10, 7, 6, 8, 7 };
        entryPattern4 = new int[] { 1, 7, 9, 5, 4 };
        entryPattern5 = new int[] { 2, 3, 6, 7, 8 };
        entryPattern6 = new int[] { 1, 5, 9, 4, 5 };

        entryPattern = new List<int[]>();
        entryPattern.Add(entryPattern1);
        entryPattern.Add(entryPattern2);
        entryPattern.Add(entryPattern3);
        entryPattern.Add(entryPattern4);
        entryPattern.Add(entryPattern5);
        entryPattern.Add(entryPattern6);

        node = new GameObject[11];

        for (int i = 0; i < 11; i++)
        {
            node[i] = GameObject.Find("Node" + (i + 1));
        }
    }

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        shipState = ShipState.LINE;
        rigidBody = GetComponent<Rigidbody2D>();
        DEFAULT_SHIP_SPEED = 0.8f;
        shipSpeed = DEFAULT_SHIP_SPEED;
        nodeTracker = 0;
        DEFAULT_LURK_TIME = 7.0f;
        lurkTimer = DEFAULT_LURK_TIME;
        entryIndex = 0;
        SHOOT_RESET = 5.0f/3;
        shootTimer = SHOOT_RESET;
        bulletForce = 50f;
    }

    void FixedUpdate() //Update ship positions, next active waypoint, 
    {
        //Debug.Log("Lurking: " + lurkTimer);
        switch (shipState)
        {
            case ShipState.ENTRY:
                if (Vector3.Distance(transform.position, node[(entryPattern[entryIndex])[nodeTracker] - 1].transform.position) >= 0.1f)
                {
                    MoveShip(node[(entryPattern[entryIndex])[nodeTracker] - 1]);
                    transform.LookAt(node[(entryPattern[entryIndex])[nodeTracker] - 1].transform.position);
                }
                else if (nodeTracker + 1 < entryPattern[entryIndex].Length)
                {
                    nodeTracker++;
                }
                else
                {
                    shipState = ShipState.LINE;
                }
                break;

            case ShipState.LINE:
                if (Vector3.Distance(transform.position, node[(entryPattern[entryIndex])[nodeTracker] - 1].transform.position) >= 0.1f)
                {
                    MoveShip(node[(entryPattern[entryIndex])[nodeTracker] - 1]);
                    transform.LookAt(player.transform.position);
                }
                else if (nodeTracker + 1 < entryPattern[entryIndex].Length)
                {
                    lurkTimer = DEFAULT_LURK_TIME;
                    shipState = ShipState.LURK;
                    nodeTracker++;
                }
                else
                {
                    lurkTimer = DEFAULT_LURK_TIME;
                    shipState = ShipState.LURK;
                    nodeTracker--;
                }
                break;

            case ShipState.LURK:
                transform.LookAt(player.transform.position);
                lurkTimer -= Time.deltaTime;
                if (lurkTimer <= 0)
                {
                    shipState = ShipState.LINE;
                }
                break;

            default:
                break;
        }
    }

    void Update() 
    {
        if (shipState == ShipState.LINE)
        {
            shipSpeed = DEFAULT_SHIP_SPEED / 2f;
        }
        else
        {
            shipSpeed = DEFAULT_SHIP_SPEED;
        }

        if (shipState == ShipState.LINE || shipState == ShipState.LURK)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = SHOOT_RESET;
            }
        }
    }

    public void MoveShip(Vector2 destinationPoint) 
    {
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, (Vector3)destinationPoint, shipSpeed * Time.deltaTime));
    }

    public void MoveShip(Vector3 destinationPoint)
    {
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, destinationPoint, shipSpeed * Time.deltaTime));
    }

    public void MoveShip(GameObject node)
    {
        rigidBody.MovePosition(Vector2.MoveTowards(transform.position, node.transform.position, shipSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().hitPoints--;
        }
    }

    private void Shoot()
    {
        var temp = Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody2D>().AddForce(((Vector2)player.transform.position - (Vector2)temp.transform.position) * bulletForce);
    }


}
