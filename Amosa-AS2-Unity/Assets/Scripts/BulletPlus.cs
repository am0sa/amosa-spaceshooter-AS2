using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlus : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 11)
        {
            if (other.gameObject.tag == "Drone")
            {
                other.transform.parent.GetComponent<ShipController>().hitPoints -= 3;
                player.GetComponent<PlayerController>().scoreEarned += 20;
            }
            

            if (other.gameObject.tag == "Turret")
            {
                other.transform.parent.GetComponent<TurretController>().hitPoints--;
                player.GetComponent<PlayerController>().scoreEarned += 50;
            }
        }
    }
}
