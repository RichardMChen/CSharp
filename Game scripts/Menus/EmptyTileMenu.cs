using UnityEngine;
using System.Collections;

public class EmptyTileMenu : MonoBehaviour {

    private string[] emptyTileMenuOptions = { "End Turn", "Back" };
    //private bool[] emptyTileMenuOptionsBool;
    private int selectIndex;
    private bool showEmptyTileMenu;
    private bool endTurnChosen;
    private bool backChosen;
    private CursorSelection cursorSelect;
    private GridTile gridTile;
    private GridWorld grid;
    private CursorMovement cursorMove;
    private GameController gameController;
    private Battle battle;

	// Use this for initialization
	void Start () 
    {
        cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        gridTile = null;
        grid = GameObject.Find("Grid").GetComponent<GridWorld>();
        cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
        //emptyTileMenuOptionsBool = new bool[emptyTileMenuOptions.Length];
        selectIndex = 0;
        showEmptyTileMenu = false;
        endTurnChosen = false;
        backChosen = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true &&
            gameController.GetDefeatState() == false)
        {
            MenuSelecting();   // Call the function that handles the menu movement

            if (Input.GetButtonDown("Confirm"))
            {
                gridTile = grid.row[cursorMove.GetCurrentRow()].column[cursorMove.GetCurrentCol()].GetComponent<GridTile>();
                if (gridTile.GetIsOccupied() == false && cursorSelect.GetMoveModeState() == false && endTurnChosen == false && backChosen == false
                    && gameController.GetAttackModeState() == false)
                {
                    showEmptyTileMenu = true;
                }
                else if (gridTile.GetIsACharWaiting() == true)
                {
                    showEmptyTileMenu = true;
                }
                /* If the accept key is pressed and the empty tile menu is shown then perform the action selected */
            }
            else if (Input.GetButtonDown("Confirm") && showEmptyTileMenu == true)
            {
                handleEmptyTileSelection();
            }
            else if (Input.GetButtonDown("Cancel") && showEmptyTileMenu == true)
            {
                showEmptyTileMenu = false;
            }
        }
	}

    void OnGUI()
    {
        /* For handling the font size of the Gui grid buttons */
        GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.fontSize = 20;

        /* If showCharActionMenu is true then show the selection grid and set the select index */
        if (showEmptyTileMenu == true)
        {
            selectIndex = GUI.SelectionGrid(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 130, 200), selectIndex, emptyTileMenuOptions, 1, guiStyle);
        }
        else
        {
            /* When the action menu is not shown, the selectIndex is reset back to 0 so that the focus is back on the top of the list of options. */
            selectIndex = 0;
            endTurnChosen = false;
            backChosen = false;
        }

        /* Makes the focus on the button that matches the current selectIndex. */
        GUI.FocusControl(emptyTileMenuOptions[selectIndex]);
    }

    void MenuSelecting()
    {
        // Get keyboard input and increase or decrease our grid integer
        if (Input.GetButtonDown("Up") && showEmptyTileMenu == true)
        {
            // Here we want to create a wrap around effect by resetting the selGridInt if it exceeds the no. of buttons
            if (selectIndex == 0)
            {
                selectIndex = emptyTileMenuOptions.Length - 1;
            }
            else
            {
                selectIndex--;
            }
        }

        if (Input.GetButtonDown("Down") && showEmptyTileMenu == true)
        {
            // Create the same wrap around effect as above but alter for down arrow
            if (selectIndex == emptyTileMenuOptions.Length - 1)
            {
                selectIndex = 0;
            }
            else
            {
                selectIndex++;
            }
        }

        /* The accepting of options in a menu*/
        if (Input.GetButtonDown("Confirm") && showEmptyTileMenu == true)
        {
            handleEmptyTileSelection();
        }

        /* Handles whether the action menu shows up or not*/
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    showCharActionMenu = true;
        //}

        //if (Input.GetButtonDown("Cancel"))
        //{
        //    if (cursorSelect.GetMoveModeState() == true)
        //    {
        //        showEmptyTileMenu = true;
        //    }
        //    else
        //    {
        //        showEmptyTileMenu = false;
        //    }
        //}
    }

    void handleEmptyTileSelection()
    {
        switch (selectIndex)
        {
            case 0: showEmptyTileMenu = false;
                    endTurnChosen = true;
                    gameController.SetPlayerCharsToWait();
                    gameController.EnemyTurnStart();
                    break;
            case 1: showEmptyTileMenu = false;
                    backChosen = true;
                    break;
        }
    }

    public void SetIsEmptyTileMenuShow(bool setETM)
    {
        showEmptyTileMenu = setETM;
    }

    public bool GetIsEmptyTileMenuShow()
    {
        return showEmptyTileMenu;
    }

    public bool GetEndTurnChoiceState()
    {
        return endTurnChosen;
    }

    public bool GetBackChoiceState()
    {
        return backChosen;
    }

}
