using UnityEngine;
using System.Collections;

public class MoveConfirmMenu : MonoBehaviour 
{
    //private GameController gameController;
    private bool showMoveConfirmationMenu;  // Boolean to determine if the move confirmation menu displays or not
    private bool showAfterMoveMenu;
    private bool yesChosen; // Boolean that determines if "Yes" option was chosen on the move confirmation menu
    private bool noChosen;  // Boolean that determines if "No" option was chosen on the move confirmation menu
    private bool attackChosen;
    private bool waitChosen;
    private string[] moveConfirmMenuOptions = { "Yes", "No"};  // Array to hold the move confirmation menu options
    private string[] afterMoveMenuOptions = {"Attack", "Wait"};
    private int moveConfirmSelectionIndex;  // Index to move through the move confirmation menu options
    private int afterMoveSelectionIndex;
    private int waitChosenCounter;
    private GameObject[] moveMarkers;
    private CursorSelection cursorSelect;  // Reference to the CursorSelection script
    private GameController gameController;
    private Battle battle;

	// Use this for initialization
	void Start () 
    {
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
        showMoveConfirmationMenu = false;
        yesChosen = false;
        noChosen = false;
        moveConfirmSelectionIndex = 0;
        afterMoveSelectionIndex = 0;
        showAfterMoveMenu = false;
        //attackChosen = false;
        waitChosen = false;
        cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("move menu: " + showMoveConfirmationMenu);
        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true)
        {
            /* To handle the movement between options on the menu */
            MenuSelecting();

            /* When then "Confirm" button is pressed and the move confirm menu is visible then perform the selected option */
            if (Input.GetButtonDown("Confirm") && showMoveConfirmationMenu == true)
            {
                handleSelection();
            }
            else if (Input.GetButtonDown("Confirm") && showAfterMoveMenu == true)
            {
                handleSelection();
            }

            if (Input.GetButtonDown("Cancel") && showAfterMoveMenu == true)
            {
                showAfterMoveMenu = false;
                GridWorld gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
                CursorSelection cursorSel = GameObject.Find("Cursor").GetComponent<CursorSelection>();
                CharacterMove charMove = GameObject.Find(cursorSel.GetSelectedPlayerCharName()).GetComponent<CharacterMove>();
                GameObject character = GameObject.Find(cursorSel.GetSelectedPlayerCharName());
                character.transform.position = gridWorld.row[charMove.GetPrevRow()].column[charMove.GetPrevCol()].transform.position;
                charMove.SetCurRowAndCol(charMove.GetPrevRow(), charMove.GetPrevCol());
                gameController.SetPreviousMenu("action");
            }
        }
        //if(noChosen == true)
        //{
        //    showMoveConfirmationMenu = false;
        //}
        //Debug.Log("wait chosen: " + waitChosen);
        //Debug.Log("waitChosen counter state: " + waitChosenCounter);
	}

    void OnGUI()
    {
        string controlName = gameObject.GetHashCode().ToString();
        GUI.SetNextControlName(controlName);
        Rect bounds = new Rect(0, 0, 0, 0);
        GUI.TextField(bounds, "", 0);

        /* GUI style for the font size of the menu */
        GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
        GUIStyle textGuiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
        textGuiStyle.fontSize = 20;

        /* Displays the box and button that make up the move confirm menu when the bool is true */
        if (showMoveConfirmationMenu == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 155, Screen.height / 2 - 150, 300, 180), "Do you wish to move here?", guiStyle);

            GUI.SetNextControlName("Yes");
            GUI.Button(new Rect(Screen.width / 2 - 127, Screen.height / 2 - 30, 110, 45), "Yes", guiStyle);

            GUI.SetNextControlName("No");
            GUI.Button(new Rect(Screen.width / 2 + 6, Screen.height / 2 - 30, 110, 45), "No", guiStyle);

            GUI.FocusControl(moveConfirmMenuOptions[moveConfirmSelectionIndex]);
            //moveConfirmSelectionIndex = GUI.SelectionGrid(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 130, 200), moveConfirmSelectionIndex, moveConfirmMenuOptions, 1, guiStyle);
        }
        else if (showAfterMoveMenu == true)
        {
            afterMoveSelectionIndex = GUI.SelectionGrid(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 130, 200), afterMoveSelectionIndex, afterMoveMenuOptions, 1, guiStyle);
        }
        else
        {
            /* When the move confirm menu is not visible then the index is set back to 0 and "No" button being chosen is set to false */
            moveConfirmSelectionIndex = 0;
            afterMoveSelectionIndex = 0;
            yesChosen = false;
            noChosen = false;
            //attackChosen = false;
            waitChosen = false;
        }
    }

    /* Changes the index based on button pressed and allows for wrap around when going over either upper or lower array bound */
    void MenuSelecting()
    {
        if (showMoveConfirmationMenu == true)
        {
            // Get keyboard input and increase or decrease our button grid integer
            if (Input.GetButtonDown("Left") && showMoveConfirmationMenu == true)
            {
                if (moveConfirmSelectionIndex == 0)
                {
                    moveConfirmSelectionIndex = moveConfirmMenuOptions.Length - 1;
                }
                else
                {
                    moveConfirmSelectionIndex--;
                }
            }

            if (Input.GetButtonDown("Right") && showMoveConfirmationMenu == true)
            {
                if (moveConfirmSelectionIndex == moveConfirmMenuOptions.Length - 1)
                {
                    moveConfirmSelectionIndex = 0;
                }
                else
                {
                    moveConfirmSelectionIndex++;
                }
            }
        }
        else if(showAfterMoveMenu == true)
        {
            // Get keyboard input and increase or decrease our button grid integer
            if (Input.GetButtonDown("Up") && showAfterMoveMenu == true)
            {
                // Here we want to create a wrap around effect by resetting the selGridInt if it exceeds the no. of buttons
                if (afterMoveSelectionIndex == 0)
                {
                    afterMoveSelectionIndex = afterMoveMenuOptions.Length - 1;
                }
                else
                {
                    afterMoveSelectionIndex--;
                }
            }

            if (Input.GetButtonDown("Down") && showAfterMoveMenu == true)
            {
                // Create the same wrap around effect as above but alter for down arrow
                if (afterMoveSelectionIndex == afterMoveMenuOptions.Length - 1)
                {
                    afterMoveSelectionIndex = 0;
                }
                else
                {
                    afterMoveSelectionIndex++;
                }
            }
        }
    }

    /* Handles the selection made and perform the action based on what was pressed */
    void handleSelection()
    {
        if(showMoveConfirmationMenu == true)
        {
            switch (moveConfirmSelectionIndex)
            {
                case 0: showMoveConfirmationMenu = false;
                        yesChosen = true;
                        CursorSelection curSel = GameObject.Find("Cursor").GetComponent<CursorSelection>();
                        CharacterMove charMove = GameObject.Find(curSel.GetSelectedPlayerCharName()).GetComponent<CharacterMove>();
                        charMove.SetCurRowAndCol(charMove.pathList[charMove.pathList.Count - 1].row, charMove.pathList[charMove.pathList.Count - 1].col);
                        break;

                case 1: showMoveConfirmationMenu = false;
                        noChosen = true;
                        break;
            }
        }
        else if(showAfterMoveMenu == true)
        {
            switch (afterMoveSelectionIndex)
            {
                case 0: showAfterMoveMenu = false;
                        PlayerAttack playerAttack = GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<PlayerAttack>();
                        gameController.SetAttackModeState(true);
                        gameController.SetDisplayAttackRangeState(true);
                        playerAttack.SetCalculateAttackRange(true);
                        gameController.SetPreviousMenu("afterMove");
                        break;

                case 1: waitChosen = true;
                        waitChosenCounter++;
                        CharacterMove charMove = GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<CharacterMove>();
                        charMove.SetPrevRowAndCol(charMove.GetCurRow(), charMove.GetCurCol());
                        moveMarkers = GameObject.FindGameObjectsWithTag("MoveMarker");
                        for (int i = 0; i < moveMarkers.Length; i++)
                        {
                            Destroy(moveMarkers[i]);
                        }
                        GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<CharacterMove>().ClearPathList();
                        GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<CharacterState>().SetIsWaiting(true);                        
                        cursorSelect.SetSelectedPlayerCharName("");
                        cursorSelect.SetMoveModeState(false);
                        showAfterMoveMenu = false;
                        break;
            }
        }
    }

#region Setters and Getters
    /* Gets the state of whether the move confirm menu is being shown or not */
    public void SetMoveConfirmationMenu(bool mc)
    {
        showMoveConfirmationMenu = mc;
    }

    /* Sets the move confirm window */
    public bool GetMoveConfirmationMenu()
    {
        return showMoveConfirmationMenu;
    }

    public void SetShowAfterMoveMenu(bool samm)
    {
        showAfterMoveMenu = samm;
    }

    public bool GetShowAfterMoveMenu()
    {
        return showAfterMoveMenu;
    }

    /* Sets the "No" choice */
    public void SetNoChoice(bool nc)
    {
        noChosen = nc;
    }

    /* Get the state of noChosen to see if the "No" option was selected or not */
    public bool GetNoChoiceState()
    {
        return noChosen;
    }

    /* Sets the "Yes" choice */
    public void SetYesChoice(bool yc)
    {
        yesChosen = yc;
    }

    /* Get the state of noChosen to see if the "No" option was selected or not */
    public bool GetYesChoiceState()
    {
        return yesChosen;
    }

    public bool GetWaitChosenState()
    {
        return waitChosen;
    }

    public void SetWaitChosenState(bool swcs)
    {
        waitChosen = swcs;
    }

    public int GetWaitChosenCounterState()
    {
        return waitChosenCounter;
    }

    public void SetWaitChosenCounterState(int wcc)
    {
        waitChosenCounter = wcc;
    }

#endregion

}
