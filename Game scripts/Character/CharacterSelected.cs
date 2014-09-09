/* Handles when the character is selected */

using UnityEngine;
using System.Collections;

public class CharacterSelected : MonoBehaviour 
{
    public bool isCharSelected;        // Name of the character that is selected
    private string selectedCharName;   // Name of the character gameObject selected
    private ActionMenu actMenu;        // Reference to Action Menu
    private CharacterState charState;  // Reference to the CharacterState
    private GameController gameController;
    //private CursorSelection cursorSelect;

	// Use this for initialization
	void Start () 
    {
        isCharSelected = false;
        selectedCharName = "";
        actMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();   // Initialize the ActionMenu variable to the Main Camera component
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        /* Initialize the CharacterState variable to the component on the current character
           script is attached to */
        charState = gameObject.GetComponent<CharacterState>();                  
        //cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        //cursorSelect = theCursor.GetComponent<CursorSelection>();
	}
	

    /* A function used to get the value of the boolean isCharSelected to 
       see if the character is selected by the cursor*/
    bool GetIsCharSelected()
    {
        return isCharSelected;
    }

    /* A function to get the name of the character that is selected*/
    public string GetSelectedCharName()
    {
        return selectedCharName;
    }

    /* Tells whether the cursor's collider is within the collider of the character*/
    void OnTriggerStay(Collider coll)
    {
        /* If the collider has the "Cursor" tag then set cursorSelect to the CursorSelection component on the cursor*/
        if (coll.gameObject.tag == "Cursor")
        {
            CursorSelection cursorSelect = coll.GetComponent<CursorSelection>();

            /* If the cursor is selecting and the cursor is on the character then make the
               isCharSelected variable true and set the name of character that the cursor is 
               currently selecting*/
            if (cursorSelect.GetIsSelected() == true)
            {
                isCharSelected = true;
                //cursorSelect.moveMode = true;
                selectedCharName = gameObject.name;
                //Debug.Log(selectedCharName);
                //if(cursorSelect.GetMoveModeState() == false)
                //{
                //    actMenu.SetIsActionMenuShow(true);
                //}
            }
            else
            {
                isCharSelected = false;
            }

            /* If the character is selected and the cursor is not in the move mode
               then display the menu if the character is not set to wait */
            if (isCharSelected == true && cursorSelect.GetMoveModeState() == false && gameController.GetAttackModeState() == false)
            {
                if (charState.GetIsWaiting() == false)
                {
                    actMenu.SetIsActionMenuShow(true);
                }
                else
                {
                    actMenu.SetIsActionMenuShow(false);
                }
            }
            else
            {
                isCharSelected = false;
                actMenu.SetIsActionMenuShow(false);                
            }
        }
    }
}
