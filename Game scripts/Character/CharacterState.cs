/* Keeps track of the character state.
   Ex. 1) Whether they are waiting. */

using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour 
{
    public bool isWaiting;  // A boolean to tell whether the character is set to wait
    public GameObject materialGameObject; // The gameobject with the material component

    private Color32 defaultColor;

	// Use this for initialization
	void Start () 
    {
        isWaiting = false;
        defaultColor = materialGameObject.renderer.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isWaiting == true)
        {
            materialGameObject.renderer.material.color = new Color32(88, 88, 81, 255);
        }
        else
        {
            materialGameObject.renderer.material.color = defaultColor;
        }
        //Debug.Log(this.name + ": " + materialGameObject.renderer.material.GetColor("_Color"));
	}

    /* A function to set the character to wait */
    public void SetIsWaiting(bool w)
    {
        isWaiting = w;
    }

    /* A function to get the status of character and see if they are waiting*/
    public bool GetIsWaiting()
    {
        return isWaiting;
    }
}
