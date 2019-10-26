using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    [SerializeField] protected int hitPoints;

    public GameManager gameManager;

    public Animator animator;

    public int scoreEarned;
    
    public float blinkRecharge;

    public float RECHARGE_TIME;

    public bool blinkIsCharging;

    public bool cannonCharge;

    public float cannonChargeTimer;

    public enum PowerLevel
    {
        one,
        two,
        three,
        super
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 1f;
        cannonChargeTimer = 0f;
        RECHARGE_TIME = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isFacingRight", false);
            movementDir += Vector2.left;
        }
        else
        {
            //not left or right
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

        //------!!!!!!!------movementDir value set------!!!!!!------
        movementDir = (movementDir.normalized * playerSpeed * Time.deltaTime);

        //------!!!!!!!------movementDir value use------!!!!!!------
        transform.position += (Vector3)movementDir;


        if (Input.GetKeyDown(KeyCode.LeftShift) && !blinkIsCharging)
        {
            blinkIsCharging = Blink(movementDir);
            blinkRecharge = RECHARGE_TIME;
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            cannonChargeTimer += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.RightShift))
        {
            int pow = (int)cannonChargeTimer;
            Shoot(pow);
        }



        //------!!!!!!-------free space continues
    }

    public bool Blink(Vector2 movementDir)
    {
        Vector2 destination = playerSpeed * ((Vector2)transform.position + movementDir);
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
        //Instantiate rapidfire bullets
    }

    public void Shoot(int pow)
    {
        PowerLevel shotPower = PowerLevel.one;
        if (pow >= 3)
        {
            shotPower = PowerLevel.super;
        }
        else if (pow >=2 )
        {
            
        }

    }




















}
