/* Determines when the cursor is selecting something, what should happen when the 
 * cursor is selecting, and to change colors depeding on the action such as when moving 
 * a character. */

using UnityEngine;
using System.Collections;

public class CursorSelection : MonoBehaviour 
{
    private bool isSelected;   // A Boolean to determine whether the current tile that the cursor is on is selected
    private bool moveMode;     // Determines whether the player decided to move a character
    private string selectedPlayerName;
    private ActionMenu actionMenu;   // References the ActionMenu component
    private MoveConfirmMenu moveConfirmMenu;  // Reference the MoveConfirmMenu script
    //private CursorMovement cursorMove;
    private EmptyTileMenu emptyTileMenu;
    private GameController gameController;
    private Battle battle;
    //private GridWorld gridWorld;
    //private GridTile gridTile;
    private CursorMovement curMove;
    //private CharacterSelected charSelect;   // References the CharacterSelected component

	// Use this for initialization
	void Start () 
    {
        actionMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();
        moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
        //cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        emptyTileMenu = GameObject.Find("Main Camera").GetComponent<EmptyTileMenu>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
        //gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
        curMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        isSelected = false;
        moveMode = false;
        selectedPlayerName = "";
        //charSelect = null;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("selected state: " + isSelected);
        //Debug.Log("movemode: " + moveMode);
        //Debug.Log("sel: " + selectedPlayerName);
        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true && gameController.GetDefeatState() == false)
        {
            PlayerSelection();
            /* If the accept key is pressed and the action menu is not shown then the current tile is selected */
            if (Input.GetButtonDown("Confirm"))
            {
                //if (GameObject.Find(selectedPlayerName) == null)
                //{
                //GridWorld gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
                //GameObject curTile = gridWorld.row[cursorMove.GetCurrentRow()].column[cursorMove.GetCurrentCol()];
                //CharacterState charState = GameObject.Find(selectedPlayerName).GetComponent<CharacterState>();
                if (moveConfirmMenu.GetWaitChosenState() == true)
                {
                    isSelected = false;
                }
                else if (actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                    emptyTileMenu.GetIsEmptyTileMenuShow() == false && emptyTileMenu.GetEndTurnChoiceState() == false &&
                    emptyTileMenu.GetBackChoiceState() == false && gameController.GetCharIsMoving() == false && gameController.CursorIsOnEnemy() == false)
                {
                    isSelected = true;

                    //Debug.Log("isSelected = " + isSelected);
                }
                //}
                //else 
                //{
                //    //CharacterState charState = GameObject.Find(selectedPlayerName).GetComponent<CharacterState>();
                //    if (actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false && 
                //        emptyTileMenu.GetIsEmptyTileMenuShow() == false && emptyTileMenu.GetEndTurnChoiceState() == false && 
                //        emptyTileMenu.GetBackChoiceState() == false && gameController.GetCharIsMoving() == false)
                //    {
                //        Debug.Log("hi. work");
                //    }
                //}
            }

            /* If the cancel key is pressed and the action menu is not shown then the current tile is deselected */
            if (Input.GetButtonDown("Cancel") && moveMode == false && actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                moveConfirmMenu.GetShowAfterMoveMenu() == false && gameController.GetCharIsMoving() == false && gameController.GetAttackModeState() == false)
            {
                isSelected = false;
                selectedPlayerName = "";
                //if(moveMode == true)
                //{
                //    moveMode = false;
                //}
                //Debug.Log("isSelected = " + isSelected);
            }

            /* If the cursor selects something... */
            if (isSelected == true)
            {
                /* ...and if the cursor is in move mode then... */
                if (moveMode == true)
                {
                    // ...the color of the cursor is a semi-transparent orange
                    renderer.material.color = new Color32(236, 145, 7, 130);
                }
                else
                {
                    // ...the color of the cursor is a semi-transparent light blue color
                    renderer.material.color = new Color32(63, 110, 246, 130);
                }
            }
            else
            {
                // ...the color of the cursor is a semi-transparent default white color
                renderer.material.color = new Color32(255, 255, 255, 130);
            }
        }
        /* ...if the character is ordered to wait then the cursor is deselected and
                   the waitChoosen variable in the action menu is set to false after Wait command is selected*/
        if (actionMenu.getWaitChoosen() == true)
        {
            isSelected = false;
            actionMenu.setWaitChoosen(false);
            selectedPlayerName = "";
        }

        else if(gameController.GetDefeatState() == true)
        {
            renderer.enabled = false;
        }

        Debug.Log("Selected player: " + selectedPlayerName);
	}

    /* Check to see if the collider is in a trigger collider of the cursor */
    void PlayerSelection()
    {
        for(int i = 0; i < gameController.playerList.Count; i++)
        {
            CharacterMove playerLocation = GameObject.Find(gameController.playerList[i].name).GetComponent<CharacterMove>();
            // If the gameobject's collider tagged with "Player" then...
            if (playerLocation.GetCurRow() == curMove.GetCurrentRow() && playerLocation.GetCurCol() == curMove.GetCurrentCol() 
                && gameController.GetAttackModeState() == false)
            {
                // Sets charSelect to the CharacterSelected script component on the colliding player
                //charSelect = GameObject.Find(colli.gameObject.name).GetComponent<CharacterSelected>();

                /* ...if the cursor is selecting and not in move mode then... */
                if (isSelected == true && moveMode == false)
                {
                    /* Set the character name variable in the action menu to the name of
                       the "Player" gameobject that the collider belongs to */
                    //actionMenu.SetCharName(colli.gameObject.name);
                    selectedPlayerName = gameController.playerList[i].name;
                    //Debug.Log(colli.gameObject.name);
                }                
            }
        }

        
        //if (colli.gameObject.tag == "Enemy")
        //{
        //    cursorOnEnemy = true;
        //}
        //else
        //{
        //    cursorOnEnemy = false;
        //}
    }

#region Setters and Getters
    /* Sets the move mode state */
    public void SetMoveModeState(bool set)
    {
        moveMode = set;
    }

    /* Gets the state of whether the cursor is in move mode or not */
    public bool GetMoveModeState()
    {
        return moveMode;
    }

    /* Set the cursor to select when called */
    public void SetIsSelected(bool setSelect)
    {
        isSelected = setSelect;
    }

    /* A function that allows other scripts to access the boolean isSelected to know
       if the cursor is currently selected something or not. */
    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void SetSelectedPlayerCharName(string spcn)
    {
        selectedPlayerName = spcn;
    }

    public string GetSelectedPlayerCharName()
    {
        return selectedPlayerName;
    }
#endregion
}
