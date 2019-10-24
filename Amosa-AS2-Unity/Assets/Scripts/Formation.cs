using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Formation : MonoBehaviour
{
    public Vector3 formationDirection1;
    public Vector3 formationDirection2;
    public Transform defaultFormation;
    public List<Vector3> formationGrid;
    public List<bool> isSpotTaken;
    public const int HEIGHT = 20;
    public const int WIDTH = 6;
    public float formationScrollSpeed;
    public bool flip;

    // Start is called before the first frame update
    void Awake()
    {
        formationScrollSpeed = 8f;
        flip = false;

        //Grid Position Initialization
        for (int c = 0; c <=HEIGHT-1 ; c+=2)
        {
            for (int r = 0; r <= WIDTH-1; r+=2)
            {
                Vector3 temp = new Vector3((float)r, 0, (float)c);
                formationGrid.Add(transform.localPosition + temp);
                isSpotTaken.Add(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= -19.5 ||  transform.position.x >= 0)
        {
            flip = !flip;
        }
        if (flip)
        {
            formationDirection1 = Vector3.MoveTowards(defaultFormation.transform.position, new Vector3(-25,0,transform.position.z), formationScrollSpeed * Time.deltaTime);
            transform.position = formationDirection1;
        }
        else
        {
            formationDirection2 = Vector3.MoveTowards(defaultFormation.transform.position, new Vector3(25,0,transform.position.z), formationScrollSpeed * Time.deltaTime);
            transform.position = formationDirection2;
        }
    }

    public List<Vector3> GetFormationGrid()
    {
        return formationGrid;
    }

    public List<bool> GetIsSpotTaken()
    {
        return isSpotTaken;
    }
}

/* GRID FOR LOOPS

        for (int c = FormationGrid.HEIGHT-1; c >= 0 ; c--)
        {
            for (int r = FormationGrid.WIDTH-1; r >= 0; r--)
            {
                m_tempShip = Instantiate(shipPrefab, new Vector3(r*5,0,c*5), Quaternion.Euler(new Vector3(0, 180, 0)));
                m_tempShip.transform.parent = defaultFormation;
            }
        }
        
         */