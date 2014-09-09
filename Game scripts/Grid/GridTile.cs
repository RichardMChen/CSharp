/* Keeps track of what is on the tile and knows whether something is occupying the
 * tile or not. */

using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour 
{
    public bool isOccupied;   // A boolean to tell if there is a something on the tile currently
    public bool viableMove;   // Boolean to tell if the tile is a viable move for a character to move to
    public bool isACharWaiting;
    public int movementCost;  // The amount of movement that must be spent to traverse the tile

	// Use this for initialization
	void Start () 
    {
        isOccupied = false;
        isACharWaiting = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(gameObject.transform.position);
	}

    void OnTriggerStay(Collider other)
    {
        /* If there is a "Player" or "Enemy" on the tile then the tile is currently occupied */
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            isOccupied = true;
            //Debug.Log("Location" + gameObject.name + ": " + transform.localPosition);
        }
        else
        {
            /* Otherwise the tile is not occupied */
            isOccupied = false;
        }

        if (other.gameObject.tag == "Player")
        {
            CharacterState charState = GameObject.Find(other.gameObject.name).GetComponent<CharacterState>();
            if (charState.GetIsWaiting() == true)
            {
                isACharWaiting = true;
            }
            else
            {
                isACharWaiting = false;
            }
        }
        else
        {
            isACharWaiting = false;
        }
    }

    /* Get the variable to determine if this tile is a viable for a character. */
    public bool GetViableMove()
    {
        return viableMove;
    }

    public bool GetIsOccupied()
    {
        return isOccupied;
    }

    public bool GetIsACharWaiting()
    {
        return isACharWaiting;
    }
}
