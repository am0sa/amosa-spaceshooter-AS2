//Created by Amosa
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // GameManager - manages active objects, ui, persistent data

    public enum ShipState
    {
        ENTRY, //Everything between instantiation and the turning point
        FOLLOW, //Follow 1st ship of attack line
        CHARGE,//Charge the player
        RAGE,  //Use of multiple waypoints
    }
    public ShipState shipState; 
    private GameManager gameManager;
    private Formation formation;
    public bool kamikazeEnabled;

    //movement variables
    public float shipSpeed;
    public float radius;
    public float rotationSpeed;
    private float chargeTimer;
    private float chargeTrigger;
    public Rigidbody rigidBody;


    //Ship Formation Variables
    public bool isInFormation;
    public GameObject formationHolder;

    // Start is called before the first frame update
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
    }

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shipState = ShipState.ENTRY;
        rigidBody = GetComponent<Rigidbody>();
        isInFormation = false;
        shipSpeed = 4f;
        chargeTimer = 4.5f;
        chargeTrigger = 5.0f;
    }

    void FixedUpdate() //Update ship positions, next active waypoint, 
    {
        switch (shipState)
        {
            case ShipState.ENTRY:

                break;

            case ShipState.FOLLOW:

                break;

            case ShipState.CHARGE:

                break;

            case ShipState.RAGE:

                break;

            default:
                break;
        }
    }

    public void Charge(GameObject player)
    {
        Vector2 moveTo = new Vector2(0, 0);

        if (Vector2.Distance(player.transform.position, transform.position) > 0.4f)
        {
            if (chargeTimer <= 0)
            {
                Kamikaze();
            }
            else
            {
                moveTo = Vector2.MoveTowards(transform.position, player.transform.position, shipSpeed * Time.deltaTime);
                chargeTimer -= Time.deltaTime;
            }
        }
        else if (Vector2.Distance(player.transform.position, transform.position) <= 0.4f)
        {
            Kamikaze();
        }
    }

    public void MoveShip(Vector3 destinationPoint) //Moves in a straight line towards point.
    {
        transform.LookAt(destinationPoint);
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, destinationPoint, shipSpeed * Time.deltaTime);
        rigidBody.MovePosition(targetPosition);
    }

    public void Kamikaze()
    {
        //shipExplode hasn't been figured out yet
        Debug.Log("KAMIKAZE!");
        Destroy(gameObject);
    }
}