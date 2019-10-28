using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 15 || transform.position.x < -15 || transform.position.y > 15 || transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.layer == 13)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                other.gameObject.GetComponent<PlayerController>().hitPoints--;
            }
            else
            {
                Debug.Log("Critical Error MF");
            }
        }
        Destroy(gameObject);
    }
}
