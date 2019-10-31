using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.position.x > 15 || transform.position.x < -15 || transform.position.y > 15 || transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Drone");
            other.gameObject.SetActive(false);
            player.GetComponent<PlayerController>().scoreEarned += 10;
        }
        else if(other.gameObject.layer == 13)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                other.gameObject.GetComponent<PlayerController>().hitPoints--;
            }
            else
            {
                Debug.Log(other.gameObject.name);
            }
        }

        Destroy(gameObject);
    }
}
