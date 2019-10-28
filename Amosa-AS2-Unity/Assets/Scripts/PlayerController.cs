﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float bulletOffset;

    protected int hitPoints;

    public GameManager gameManager;
    public Animator animator;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bulletSuper;
    private Transform forwardHolder;

    public Vector2 forward;

    public int scoreEarned;
    
    public float blinkRecharge;
    public float RECHARGE_TIME;
    public float cannonChargeTimer;
    public float rapidFireDelay;
    public float bulletForce;

    public bool blinkIsCharging;
    public bool cannonCharge;
    public bool isFacingRight;

    public enum PowerLevel
    {
        one,
        two,
        three,
        super
    }

    void Start()
    {
        hitPoints = 2;
        playerSpeed = 3f;
        cannonChargeTimer = 0f;
        RECHARGE_TIME = 3f;
        bulletForce = 3.5f;
        forward = new Vector2(0, 0);
        forwardHolder = transform.Find("forwardHolder");
    }

    void FixedUpdate()
    {
        isFacingRight = animator.GetBool("isFacingRight");
    }

    void Update() 
    {
        if (isFacingRight)
        {
            forward = Vector3.right;
        }
        else
        {
            forward = Vector3.left;
        }

        if (blinkIsCharging)
        {
            blinkRecharge -= Time.deltaTime;

            if(blinkRecharge <= 0)
            {
                blinkIsCharging = false;
            } 
        }

        var movementDir = new Vector2(0, 0);



        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("isFacingRight", true);
            movementDir += Vector2.right;
            forwardHolder.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isFacingRight", false);
            movementDir += Vector2.left;
            forwardHolder.rotation = new Quaternion(0, 180, 0, 0);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isRising", true);
            movementDir += Vector2.up;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("isRising", false);
            animator.SetBool("isFalling", true);
            movementDir += Vector2.down;
        }
        else
        {
            animator.SetBool("isRising", false);
            animator.SetBool("isFalling", false);
            //no change to movementDir
        }

        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.C))
        {
            Shoot();
        }
        else
        {
            rapidFireDelay = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && !blinkIsCharging)
        {
            blinkIsCharging = Blink(movementDir);
            blinkRecharge = RECHARGE_TIME;
        }

        if (Input.GetKeyUp(KeyCode.C) && !Input.GetKey(KeyCode.Space))
        {
            Debug.Log("SpaceUp)");
            int pow = (int)(cannonChargeTimer * 2.5);
            Shoot(pow);
            cannonChargeTimer = 0;
        }

        if (Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.Space))
        {
            cannonChargeTimer += Time.deltaTime;
        }


        movementDir = (movementDir.normalized * playerSpeed * Time.deltaTime);
        transform.position += (Vector3)movementDir;
    }

    public bool Blink(Vector2 movementDir)
    {
        Vector2 destination = ((Vector2)transform.position + playerSpeed * playerSpeed * movementDir);
        if ((Vector2)transform.position == destination)
        {
            return false;
        }
        else
        {
            transform.position = (Vector3)destination;
            return true;
        }
    }

    public void Shoot()
    {
        if (rapidFireDelay <=0)
        {
            Shoot(0);
            rapidFireDelay = RECHARGE_TIME / 30; 
        }
        else
        {
            rapidFireDelay -= Time.deltaTime;
        }
    }

    public void Shoot(int pow)
    {
        PowerLevel shotPower = PowerLevel.one;

        GameObject temp;

        if (pow >= 3)
        {
            shotPower = PowerLevel.super;
        }
        else if (pow >=2 )
        {
            shotPower = PowerLevel.three;
        }
        else if (pow >=1)
        {
            shotPower = PowerLevel.two;
        }
        else
        {
            shotPower = PowerLevel.one;
        }

        switch (shotPower)
        {
            case PowerLevel.one:
                temp = Instantiate(bullet1, forwardHolder);
                temp.GetComponent<Rigidbody2D>().AddForce(forward * bulletForce * 200);
                temp.transform.SetParent(GameObject.Find("Independent").transform);
                break;

            case PowerLevel.two:
                temp = Instantiate(bullet2, forwardHolder);
                temp.GetComponent<Rigidbody2D>().AddForce(forward * bulletForce * 200);
                temp.transform.SetParent(GameObject.Find("Independent").transform);
                break;

            case PowerLevel.three:
                temp = Instantiate(bullet3, forwardHolder);
                temp.GetComponent<Rigidbody2D>().AddForce(forward * bulletForce * 200);
                temp.transform.SetParent(GameObject.Find("Independent").transform);
                break;

            case PowerLevel.super:
                temp = Instantiate(bulletSuper, forwardHolder);
                temp.GetComponent<Rigidbody2D>().AddForce(forward * bulletForce * 200);
                temp.transform.SetParent(GameObject.Find("Independent").transform);
                break;
            default:
                break;
            
        }
        
    }




















}
