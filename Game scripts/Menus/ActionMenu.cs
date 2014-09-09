/* Handles the GUI menu that pops up when a character is selected. */

using UnityEngine;
using System.Collections.Generic;

public class ActionMenu : MonoBehaviour 
{
    //private List<string> actionMenuOptions = new List<string>();
    private string[] actionMenuOptions = {"Move", "Attack", "Items", "Wait"};   // An array to store and list options when a character is selected and can take action
    private int selectIndex;   // The index to keep track as to what the button should have focus
    private bool showCharActionMenu;   // Boolean to determine if the action menu is showing
    private CursorSelection cursorSel;   // Reference to the CursorSelection script
    private CursorMovement cursorMove;   // Reference to the CursorMovement script
    private CharacterState charState;   // Reference to the CharacterState script
    private GridWorld gridWorld;   // Reference to the GridWorld script
    private MoveConfirmMenu moveConfirmMenu;  // Reference the MoveConfirmationMenu script
    private GameController gameController;
    private Battle battle;
    //private string selectedCharacterName;   // String to hold the name of the character currently being selected
    //private string movingCharacter;         // The character that was chosen to move 
    //private bool[] actionMenuOptionsBool;
    private bool moveChoosen;   // A boolean to determine if the move option was choosen
    private bool waitChoosen;    // A boolean to determine if the wait option was choosen
    private GUIStyle guiStyle;   // GUIStyle to help with font size
    private GameObject[] moveMarkers;   // An array to hold the movement markers to be destroyed when "Cancel" is pressed while in move mode

	// Use this for initialization
	void Start () 
    {
        cursorSel = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
        moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
        //actionMenuOptionsBool = new bool[actionMenuOptions.Length];
        selectIndex = 0;
        showCharActionMenu = false;
        waitChoosen = false;
        //selectedCharacterName = "";
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("selectedCharacterName is " + selectedCharacterName);
        //Debug.Log("moveChoosen is: " + moveChoosen);
        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true)
        {

            MenuSelecting();   // Call the function that handles the menu movement

            /* If the accept key is pressed and the action menu is shown then perform the action selected */
            if (Input.GetButtonDown("Confirm") && showCharActionMenu == true)
            {
                handleSelection();
            }

            /* If the cancel key is pressed... */
            if (Input.GetButtonDown("Cancel") && moveConfirmMenu.GetMoveConfirmationMenu() == false && moveConfirmMenu.GetShowAfterMoveMenu() == false &&
                gameController.GetCharIsMoving() == false)
            {
                /* ...If the cancel key is pressed while the cursor is in move mode then:
                      The action menu is shown, move mode is set to false, moveChoosen is set
                      to false, the cursor is set back to the position of the tile that the 
                      character that was selected to move is at. */
                if (cursorSel.GetMoveModeState() == true && gameController.GetPreviousMenu() == "action")
                {
                    showCharActionMenu = true;
                    cursorSel.SetMoveModeState(false);
                    moveChoosen = false;
                    GameObject.Find("Cursor").transform.position = gridWorld.row[cursorMove.GetPrevRow()].column[cursorMove.GetPrevCol()].transform.position;
                    cursorMove.SetCurrentRow(cursorMove.GetPrevRow());
                    cursorMove.SetCurrentCol(cursorMove.GetPrevCol());

                    moveMarkers = GameObject.FindGameObjectsWithTag("MoveMarker");
                    for (int i = 0; i < moveMarkers.Length; i++)
                    {
                        Destroy(moveMarkers[i]);
                    }
                    GameObject.Find(cursorSel.GetSelectedPlayerCharName()).GetComponent<CharacterMove>().ClearPathList();
                }
                else
                {
                    /* ...If the cancel key is pressed when the action menu is showing then make is invisible */
                    showCharActionMenu = false;
                    //gameController.SetPreviousMenu("action");
                    //selectedCharacterName = "";
                    //cursorSel.SetSelectedPlayerCharName("");
                }
            }
        }
	}

    void OnGUI()
    {
        string controlName = gameObject.GetHashCode().ToString();
        GUI.SetNextControlName(controlName);
        Rect bounds = new Rect(0, 0, 0, 0);
        GUI.TextField(bounds, "", 0);

        /* For handling the font size of the Gui grid buttons */
        guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.fontSize = 20;

        /* If showCharActionMenu is true then show the selection grid and set the select index */
        if (showCharActionMenu == true)
        {
            selectIndex = GUI.SelectionGrid(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 130, 200), selectIndex, actionMenuOptions, 1, guiStyle);
        }
        else
        {
            /* When the action menu is not shown, the selectIndex is reset back to 0 so that the focus is back on the top of the list of options. */
            selectIndex = 0;
        }

        /* Makes the focus on the button that matches the current selectIndex. */
        GUI.FocusControl(actionMenuOptions[selectIndex]);
    }

    void MenuSelecting()
    {
        // Get keyboard input and increase or decrease our button grid integer
        if (Input.GetButtonDown("Up") && showCharActionMenu == true)
        {
            // Here we want to create a wrap around effect by resetting the selGridInt if it exceeds the no. of buttons
            if (selectIndex == 0)
            {
                selectIndex = actionMenuOptions.Length - 1;
            }
            else
            {
                selectIndex--;
            }
        }

        if (Input.GetButtonDown("Down") && showCharActionMenu == true)
        {
            // Create the same wrap around effect as above but alter for down arrow
            if (selectIndex == actionMenuOptions.Length - 1)
            {
                selectIndex = 0;
            }
            else
            {
                selectIndex++;
            }
        }
    }

    /* Handles the selection of the option selected from the action based on the current selectIndex. */
    void handleSelection()
    {
        switch (selectIndex)
        {
            /* If "Move" is chosen, then... */
            case 0: cursorSel.SetMoveModeState(true);
                    moveChoosen = true;
                    cursorMove.SetPrevRow(cursorMove.GetCurrentRow());
                    cursorMove.SetPrevCol(cursorMove.GetCurrentCol());
                    gameController.SetPreviousMenu("action");
                    showCharActionMenu = false;
                    break;

            /* If "Attack" is chosen, then... */
            case 1: PlayerAttack playerAttack = GameObject.Find(cursorSel.GetSelectedPlayerCharName()).GetComponent<PlayerAttack>();
                    gameController.SetAttackModeState(true);
                    gameController.SetDisplayAttackRangeState(true);
                    playerAttack.SetCalculateAttackRange(true);
                    gameController.SetPreviousMenu("action");
                    showCharActionMenu = false;
                    break;

            /* If "Items" is chosen, then... */
            case 2: Debug.Log(selectIndex + " - " + actionMenuOptions[selectIndex] + " was choosen");
                    break;

            /* If "Wait" is chosen, then... */
            case 3: //Debug.Log(selectIndex + " - " + actionMenuOptions[selectIndex] + " was choosen");
                    showCharActionMenu = false;
                    waitChoosen = true;
                    charState = GameObject.Find(cursorSel.GetSelectedPlayerCharName()).GetComponent<CharacterState>();
                    charState.SetIsWaiting(true);
                    break;
        }
    }

    /* Sets the action menu to show or not */
    public void SetIsActionMenuShow(bool setAcM)
    {
        showCharActionMenu = setAcM;
    }

    /* Gets the bool to see if the action menu is showing or not */
    public bool GetIsActionMenuShow()
    {
        return showCharActionMenu;
    }

    ///* Sets the character name of the character that the action menu is being shown for. */
    //public void SetCharName(string s)
    //{
    //    selectedCharacterName = s;
    //}

    ///* Gets the character name */
    //public string GetCharName()
    //{
    //    return selectedCharacterName;
    //}

    /* Sets waitChoosen to true or false */
    public void setWaitChoosen(bool w)
    {
        waitChoosen = w;
    }

    /* Gets the waitChoosen variable to see if the "Wait" command was chosen. */
    public bool getWaitChoosen()
    {
        return waitChoosen;
    }

    /* Sets moveChoosen to true or false */
    public void SetMoveChoosen(bool mc)
    {
        moveChoosen = mc;
    }

    /* Gets the variable to see if the "Move" command was chosen. */
    public bool GetMoveChoosen()
    {
        return moveChoosen;
    }
}
