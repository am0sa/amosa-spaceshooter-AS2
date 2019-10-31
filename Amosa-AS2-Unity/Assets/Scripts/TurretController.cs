using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public enum ShipState
    {
        ENTRY, //Everything between instantiation and the turning point
        LINE, //Follow 1st ship of attack line
        RAGE,  //Use of multiple waypoints
    }
    public ShipState shipState; 
    private GameManager gameManager;
    public GameObject enemyBulletPrefab;
    private Formation formation;
    public bool kamikazeEnabled;

    //movement variables
    public float shipSpeed;
    public float radius;
    private float timer;
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

    public float bulletForce;


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
            node[i] = GameObject.Find("Node" + i);
        }
    }

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        shipState = ShipState.ENTRY;
        rigidBody = GetComponent<Rigidbody2D>();
        shipSpeed = 1f;
        nodeTracker = 0;
        timer = 0f;
        entryIndex = 0;
    }

    void FixedUpdate() //Update ship positions, next active waypoint, 
    {
  
        switch (shipState)
        {
            case ShipState.ENTRY:
                if (Vector3.Distance(transform.position, node[(entryPattern[entryIndex])[nodeTracker] - 1].transform.position) >= 0.1f)
                {
                    MoveShip(node[(entryPattern[entryIndex])[nodeTracker] - 1]);
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
                }
                else if (nodeTracker + 1 < entryPattern[entryIndex].Length)
                {
                    nodeTracker++;
                }
                else
                {
                    nodeTracker--;
                }
                break;

            case ShipState.RAGE:

                break;

            default:
                break;
        }
    }

    public void MoveShip(Vector2 destinationPoint) 
    {
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, (Vector3)destinationPoint, shipSpeed * Time.deltaTime));
        transform.LookAt(destinationPoint);
    }

    public void MoveShip(Vector3 destinationPoint)
    {
        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, destinationPoint, shipSpeed * Time.deltaTime));
        transform.LookAt(destinationPoint);
    }

    public void MoveShip(GameObject node)
    {
        rigidBody.MovePosition(Vector2.MoveTowards(transform.position, node.transform.position, shipSpeed * Time.deltaTime));
        transform.LookAt(node.transform.position);
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
        var temp = Instantiate(enemyBulletPrefab, transform.position + Vector3.forward, transform.rotation);
        temp.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce);
    }


}
