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

    public enum powerLevel
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        movementDir = (movementDir.normalized * playerSpeed * Time.deltaTime);
        transform.position += (Vector3)movementDir;
    }
}
