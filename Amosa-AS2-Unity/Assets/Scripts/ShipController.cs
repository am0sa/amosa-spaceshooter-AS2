//Created by Amosa
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Ship flight type (Ship States)
    public enum AttackStage
    {
        ENTRY, //Everything between instantiation and the turning point
        ORBIT, //Loop around a point
        SYNC,  //Choose and then enter a spot in the attack formation
        CHARGE,//Charge the player
        DEMO,  //Use of multiple waypoints
    };

    public int attackStage; //{ get{ return attackStage; } set { attackStage = value; } }
    private int randomIntMax14;
    private int randomFormationSlot;
    private GameManager gameManager;
    private Formation formation;
   
    //movement variables
    public Vector3 shipPosition;
    public Vector3 center;
    public Vector3 axis;
    public Vector3 velocity;
    public float yPos;
    public float xPos;
    public float yRot1;
    public float yRot2;
    public float shipSpeed;
    public float radius;
    public float rotationSpeed; 
    public List<Vector3> flightPattern; //points for ship attack patterns
    public Rigidbody rigidBody;
    private float chargeTimer;
    private float chargeTrigger;
    public int[] demoPoints = {0,0,0,0,0};
    public int demoTracker;


    //Ship Formation Variables
    public bool isInFormation;   
    public GameObject formationHolder;

    // Start is called before the first frame update
    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        formation = GameObject.Find("defaultFormation").GetComponent<Formation>();
        flightPattern = gameManager.GetFlightPattern();
        attackStage = (int)AttackStage.ENTRY;
        rigidBody = GetComponent<Rigidbody>();
        isInFormation = false;
        axis = Vector3.up;
        shipSpeed = 40f;
        radius = 5.0f;
        rotationSpeed = (float)(shipSpeed*radius)*2; //200f;
        randomIntMax14 = Random.Range(0,14);
        randomFormationSlot = Random.Range(0, formation.formationGrid.Count);
        chargeTimer = 0f;
        chargeTrigger = 11.0f;
        demoPoints[0] = 1;
        demoPoints[1] = 4;
        demoPoints[2] = 8;
        demoPoints[3] = 5;
        demoPoints[4] = 1;
        demoTracker = 0;
    }

    void FixedUpdate() //Update ship positions, next active waypoint, 
    {
        yPos = transform.position.z;
        xPos = transform.position.x;
        velocity = rigidBody.velocity;
        yRot2 = yRot1;
        yRot1 = transform.rotation.y;
        chargeTimer += Time.deltaTime;

        switch (attackStage)
        {
            case (int)AttackStage.ENTRY:
                
                if (Vector3.Distance(transform.position, flightPattern[randomIntMax14]) >= radius+0.1)
                {
                    MoveShip(flightPattern[randomIntMax14]);
                }
                else
                {
                    attackStage = (int)AttackStage.ORBIT;
                }
                break;
            
            case (int)AttackStage.ORBIT:
                if (yRot1*yRot2 <= 0)
                {
                    attackStage = (int)AttackStage.SYNC;
                }
                else
                {
                    OrbitPoint(flightPattern[randomIntMax14]);
                }
                break;
          
            case (int)AttackStage.SYNC:
                    AddToFormation(randomFormationSlot);
                break;
          
            case (int)AttackStage.CHARGE:
                chargeTimer += Time.deltaTime;
                if (chargeTimer >= chargeTrigger)
                {
                    transform.SetParent(null);
                    MoveShip(new Vector3(transform.position.x, transform.position.y, transform.position.z - 10));
                }
                break;

            case (int)AttackStage.DEMO:
            if (Vector3.Distance(transform.position, flightPattern[demoPoints[demoTracker]]) >= 0.1f)
            {
                MoveShip(flightPattern[demoPoints[demoTracker]]);
            }
            else if (demoTracker + 1 < demoPoints.Length)
            {
                demoTracker++;
            }
            else
            {
                attackStage = (int)AttackStage.SYNC;
            }
                break;
            default:
                break;
        }
        if (yPos <= -45.0f)
        {
            Destroy(transform.gameObject);
        }
    }

    public void MoveShip(Vector3 destinationPoint) //Moves in a straight line towards point.
    {
        float tempDistance = Vector3.Distance(transform.position, destinationPoint);
        float tempAngle = Mathf.Atan(radius/tempDistance);
        Vector3 tempVector = destinationPoint;
        if (attackStage == (int)AttackStage.ENTRY)
        {
            tempVector.x += (Mathf.Cos(tempAngle) * radius);
            tempVector.z += (Mathf.Sin(tempAngle) * radius);
        }
        transform.LookAt(tempVector);
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, tempVector, shipSpeed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(targetPosition);
    }

    public void OrbitPoint(Vector3 position) //Orbits around the waypoint for a certain angle
    {
        center = position;
        transform.RotateAround(center, axis, rotationSpeed * Time.deltaTime);
        var desiredPosition = (transform.position - center).normalized * radius + center;
        transform.LookAt(transform.localPosition);
    }

    public void AddToFormation(int formationSpot = 0)
    {
        int spotHolder = formationSpot;
        if (!formation.isSpotTaken[formationSpot])
        {
            MoveShip(formation.formationGrid[formationSpot]);
        }
        else
        {
            for (int i = 0; i < formation.formationGrid.Count ; i++)
            {
                if (formation.isSpotTaken[formationSpot])
                {                
                    spotHolder = i;
                    MoveShip(formation.formationGrid[spotHolder]);
                    break;
                }
            }
        }
        
        if (Vector3.Distance(transform.position, formation.formationGrid[spotHolder]) == 0f)
        {
            transform.LookAt(new Vector3(transform.position.x, 0, -100), new Vector3(0, 1, 0));
            transform.parent = formation.defaultFormation;
            attackStage = (int)AttackStage.CHARGE;
            formation.isSpotTaken[spotHolder] = true;
        }
    }
}
