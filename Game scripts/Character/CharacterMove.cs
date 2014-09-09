using UnityEngine;
using System.Collections.Generic;

public class Position
{
    public int row;  // The row of the marker
    public int col;  // The col of the marker
    public int markerTimeSelected = 0;  // An int to keep track of how many times a marker is selected. 
                                        // To help with being able to select the marker another time after being set.
}

public class CharacterMove : MonoBehaviour 
{
    private GridWorld grid;             
    private CursorMovement cursorMove;  // Reference the cursorMove script in order to know where to place the movement markers
    private CursorSelection cursorSelection;  // Reference the cursorSelection script in order to determine when cursor is in move mode
    private CharacterStatus charStats;  // Reference the CharacterStatus script to know what the movement range for the character selected to move
    //private ActionMenu actionMenu;  // References the ActionMenu script
    private MoveConfirmMenu moveConfirmMenu; // Reference the MoveConfirmationMenu script
    private GridTile gridTile;
    private GameController gameController;
    private Battle battle;
    private bool showMoveCounterText;
    private bool performMovement;
    public bool hasMoved;
    //private bool hitLastMarker;
    //private bool showMoveConfirmationMenu;
    private int moveCounter;   // Stores the movement amount for the current character
    
    // Temperarily public for debugging
    public int prevRow;  // Used to keep track of the previous 
    public int prevCol;

    public List<Position> pathList = new List<Position>();  // A path list to hold the positions(row, col) of the path markers
    public List<GameObject> pathListGameObjects = new List<GameObject>();  // A path list to hold the path marker gameobjects

    public int startRow;  // Starting row that the character starts at when the map starts
    public int startCol;  // Starting column that the character starts at when the map starts
    public int curRow;  // Current row that the character is at
    public int curCol;  // Current row that the character is at
    public float moveSpeed;
    int movePathCounter;
    //public GameObject moveMarker;

    //public bool moveAble = true;

    // Use this for initialization
    void Start()
    {
        curRow = startRow;
        curCol = startCol;
        prevRow = curRow;
        prevCol = curCol;
        grid = GameObject.Find("Grid").GetComponent<GridWorld>();
        cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        cursorSelection = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        charStats = this.GetComponent<CharacterStatus>();
        //actionMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();
        moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
        gridTile = null;
        moveCounter = charStats.GetMovementRange();
        showMoveCounterText = false;
        performMovement = false;
        hasMoved = false;
        //hitLastMarker = false;
        movePathCounter = 0;
        //showMoveConfirmationMenu = false;

        transform.position = grid.row[startRow].column[startCol].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //gridTile = grid.row[4].column[4].gameObject.GetComponent<GridTile>();
        //Debug.Log("Condition: " + gridTile.GetIsOccupied());
        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true)
        {

            /* If the cursor is in move mode and the character selected is the character that this script is attached to so that
             * only the selected character places movement markers and moves*/
            if (cursorSelection.GetMoveModeState() == true && cursorSelection.GetSelectedPlayerCharName() == gameObject.name && gameController.GetAttackModeState() == false)
            {
                showMoveCounterText = true;  // Displays the move counter for the character this script is attached to

                /* If the path list is empty and gameobject path list is empty, then create a movement marker at the 
                 * selected-to-be moved character's starting position */
                if (pathList.Count == 0 && pathListGameObjects.Count == 0)
                {
                    GameObject startMoveMarker = null;
                    Position startMoveMarkerSetPos = new Position();
                    startMoveMarker = Instantiate(Resources.Load("MovementMarker"), grid.row[curRow].column[curCol].transform.position, Quaternion.identity) as GameObject;
                    startMoveMarkerSetPos.row = curRow;
                    startMoveMarkerSetPos.col = curCol;
                    pathList.Add(startMoveMarkerSetPos);
                    pathListGameObjects.Add(startMoveMarker);
                }

                /* To add on the movement markers' counter variable so that when placing down new markers the move confirm menu won't show though the new ones 
                 * would technically be the last in the path list */
                if (Input.GetButtonDown("Confirm") && isOnLastPathMarker() && pathList.Count > 0)
                {
                    pathList[pathList.Count - 1].markerTimeSelected++;
                }

                /* If the last move marker on the path is selected then display the move confirmation message */
                if (Input.GetButtonDown("Confirm") && isOnLastPathMarker() && pathList.Count > 1 && pathListGameObjects.Count > 0 && pathList[pathList.Count - 1].markerTimeSelected > 0 &&
                    moveConfirmMenu.GetYesChoiceState() == false && moveConfirmMenu.GetNoChoiceState() == false && moveConfirmMenu.GetShowAfterMoveMenu() == false && performMovement == false
                    && gameController.GetAttackModeState() == false)
                {
                    //Debug.Log("Hi " + isOnLastPathMarker());
                    moveConfirmMenu.SetMoveConfirmationMenu(true);
                }

                /* If the "UndoMove" button is pressed, then remove the last element in the path lists and add back the movement cost back to the move counter */
                else if (Input.GetButtonDown("UndoMove") && moveConfirmMenu.GetMoveConfirmationMenu() == false && pathList.Count > 1 && pathListGameObjects.Count > 1 &&
                    gameController.GetCharIsMoving() == false && moveConfirmMenu.GetShowAfterMoveMenu() == false)
                {
                    pathList.RemoveAt(pathList.Count - 1);
                    Destroy(pathListGameObjects[pathListGameObjects.Count - 1]);
                    pathListGameObjects.RemoveAt(pathListGameObjects.Count - 1);
                    moveCounter++;
                }

                /* If the "Yes" is chosen on the move confirm menu then the selected character is set to move along the path */
                if (moveConfirmMenu.GetYesChoiceState() == true)
                {
                    performMovement = true;
                }

                /* If the "Confirm" button is pressed, the selected character has movement still available, and the marker to be placed is adjacent
                 * to the last marker in the path, then place the marker down in that tile*/
                else if (Input.GetButtonDown("Confirm") && (moveCounter > 0) && checkViableAdjacentToLastMarker() == true)
                {
                    gridTile = grid.row[cursorMove.GetCurrentRow()].column[cursorMove.GetCurrentCol()].GetComponent<GridTile>();
                    //Instantiate(moveMarker, cursorMove.transform.position, Quaternion.identity);
                    /* If the marker is being placed on an unoccupied tile and the tile is not already on the path, then place the movement marker and 
                     * subtract movement cost from total movement counter */
                    if (gridTile.GetIsOccupied() == false && CheckIfOnPath() == false)
                    {
                        GameObject newMoveMarker = null;
                        Position moveMarkerSetPos = new Position();
                        newMoveMarker = Instantiate(Resources.Load("MovementMarker"), grid.row[cursorMove.GetCurrentRow()].column[cursorMove.GetCurrentCol()].transform.position, Quaternion.identity) as GameObject;
                        moveMarkerSetPos.row = cursorMove.GetCurrentRow();
                        moveMarkerSetPos.col = cursorMove.GetCurrentCol();
                        pathList.Add(moveMarkerSetPos);
                        pathListGameObjects.Add(newMoveMarker);
                        moveCounter--;
                    }
                    //Debug.Log("Check: " + CheckIfOnPath());
                    //Debug.Log("Condition: " + gridTile.GetIsOccupied());
                }
                //else if(gameObject.transform.position == pathListGameObjects[pathListGameObjects.Count - 1].transform.position)
                //{
                //    Debug.Log("On last marker. Show after menu.");
                //}

                /* If the character is to move, then perform the movement */
                if (performMovement == true)
                {
                    gameController.SetCharIsMoving(true);
                    Moving();
                }
                else
                {
                    movePathCounter = 0;
                }

                //Debug.Log("hasMoved: " + hasMoved);

                //Instantiate(Resources.Load("MovementMarker"), cursorMove.transform.position, Quaternion.identity);
                //Debug.Log(this.name + " Move: " + charStats.GetMovementRange());
            }
            else
            {
                showMoveCounterText = false;
                moveCounter = charStats.GetMovementRange();
            }

            /*Last marker in path list is a different color */
            for (int i = 0; i < pathListGameObjects.Count; i++)
            {
                pathListGameObjects[i].renderer.material.color = new Color32(255, 41, 41, 198);
            }
            if (pathListGameObjects.Count != 0)
            {
                pathListGameObjects[pathListGameObjects.Count - 1].renderer.material.color = new Color32(0, 255, 192, 198);
            }

            /* Debug path list */
            //Debug.Log("Path Size: " + pathList.Count);
            //for (int i = 0; i < pathList.Count; i++)
            //{
            //    Debug.Log("Row: " + pathList[i].row + " Col: " + pathList[i].col + " Times: " + pathList[i].markerTimeSelected);
            //}

            /* Debugging the UndoMove */
            //if (pathListGameObjects.Count != 0)
            //{
            //    Debug.Log("Gameobject count: " + pathListGameObjects.Count);
            //}
            //if(pathList.Count != 0)
            //{
            //    Debug.Log("Last path: " + pathList[pathList.Count - 1].row + ", " + pathList[pathList.Count - 1].col + " Count:" + pathList.Count);
            //}
        }
    }

    /* Displays the move counter for the selected character. 
       Displays the move confirmation menu.*/
    void OnGUI()
    {
        string controlName = gameObject.GetHashCode().ToString();
        GUI.SetNextControlName(controlName);
        Rect bounds = new Rect(0, 0, 0, 0);
        GUI.TextField(bounds, "", 0);

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        if(showMoveCounterText == true && cursorSelection.GetMoveModeState() == true && cursorSelection.GetSelectedPlayerCharName() == gameObject.name)
        {
            GUI.Label(new Rect(Screen.width * (1f/30f),Screen.height * (0.1f/6.3f), 100, 100),"Movement: " + moveCounter, guiStyle);
        }
    }

    /* Clear the path lists */
    public void ClearPathList()
    {
        pathList.Clear();
        pathListGameObjects.Clear();
    }

    /* Checks to see if the tile being selected already has a move marker on it */
    public bool CheckIfOnPath()
    {
        bool onPath = false;
        for (int i = 0; i < pathList.Count; i++)
        {
            if (cursorMove.GetCurrentRow() == pathList[i].row && cursorMove.GetCurrentCol() == pathList[i].col)
            {
                onPath = true;
            }
        }
        return onPath;
    }

    /* Checks to see if the cursor's current location is the same as the last marker on the path list */
    public bool isOnLastPathMarker()
    {
        bool isLast = false;  // A boolean to see if the cursor is on the last marker in the path list

        if (pathList.Count != 0)
        {
            if ((cursorMove.GetCurrentRow() == pathList[pathList.Count - 1].row) && (cursorMove.GetCurrentCol() == pathList[pathList.Count - 1].col))
            {
                isLast = true;
            }
            else
            {
                isLast = false;
            }            
        }
        return isLast;
    }

    /* Check if the cursor's position is adjacent to last marker in the path and if it is then a movement marker may be placed there */
    public bool checkViableAdjacentToLastMarker()
    {
        bool isAdjacent = false;

        if (pathList.Count != 0)
        {
            // up from the last path marker
            if (pathList[pathList.Count - 1].row == cursorMove.GetCurrentRow() + 1 && pathList[pathList.Count - 1].col == cursorMove.GetCurrentCol() && CheckIfOnPath() == false)
            {
                isAdjacent = true;
                //Debug.Log("Can place up");
            }

            // down from the last path marker
            else if (pathList[pathList.Count - 1].row == cursorMove.GetCurrentRow() - 1 && pathList[pathList.Count - 1].col == cursorMove.GetCurrentCol() && CheckIfOnPath() == false)
            {
                isAdjacent = true;
                //Debug.Log("Can place down");
            }

            // left from the last path marker
            else if (pathList[pathList.Count - 1].row == cursorMove.GetCurrentRow() && pathList[pathList.Count - 1].col == cursorMove.GetCurrentCol() + 1 && CheckIfOnPath() == false)
            {
                isAdjacent = true;
                //Debug.Log("Can place left");
            }

            // right from the last path marker
            else if (pathList[pathList.Count - 1].row == cursorMove.GetCurrentRow() && pathList[pathList.Count - 1].col == cursorMove.GetCurrentCol() - 1 && CheckIfOnPath() == false)
            {
                isAdjacent = true;
                //Debug.Log("Can place right");
            }
        }
        return isAdjacent;
    }

    /* Performs the movement through the path */
    public void Moving()
    {
        if (movePathCounter < pathListGameObjects.Count)
        {
            /* The position is set to each movement marker in the path list */
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pathListGameObjects[movePathCounter].transform.position, moveSpeed * Time.deltaTime);
            
            /* If the character has reached it's target marker then it adds to the counter and moves to the next marker in the path list */
            if (gameObject.transform.position == pathListGameObjects[movePathCounter].transform.position)
            {
                //Debug.Log("move path count: " + movePathCounter);
                movePathCounter++;
            }
        }
        else
        {
            /* ...else there is no movement being performed by the character */
            performMovement = false;
            gameController.SetCharIsMoving(false);
        }

        /* If the character's position equals to the position of the last marker on the path, then display the move confirm menu and set that the character
         * has been moved */
        if (gameObject.transform.position == pathListGameObjects[pathListGameObjects.Count - 1].transform.position && gameController.GetCharIsMoving() == false)
        {
            //Debug.Log("Display after move menu.");
            moveConfirmMenu.SetShowAfterMoveMenu(true);
            hasMoved = true;
        }
    }

    /* Sets the state of whether the character has moved */
    public void SetHasMovedState(bool hms)
    {
        hasMoved = hms;
    }

    /* Gets the state of whether the character has moved */
    public bool GetHasMovedState()
    {
        return hasMoved;
    }

    public void SetCurRowAndCol(int r, int c)
    {
        curRow = r;
        curCol = c;
    }

    public void SetPrevRowAndCol(int pr, int pc)
    {
        prevRow = pr;
        prevCol = pc;
    }

    public int GetPrevRow()
    {
        return prevRow;
    }

    public int GetPrevCol()
    {
        return prevCol;
    }

    public int GetCurRow()
    {
        return curRow;
    }

    public int GetCurCol()
    {
        return curCol;
    }

    //void DisplayMovementRange()
    //{
    //    for (int i = 1; i <= moveCount; i++)
    //    {
    //        if (cursorMove.GetCurrentRow() + i < grid.row.Length)
    //        {
    //            Instantiate(Resources.Load("MovementMarker"), grid.row[cursorMove.GetCurrentRow() + i].column[cursorMove.GetCurrentCol()].transform.position, Quaternion.identity);
    //        }

    //        if (cursorMove.GetCurrentRow() - i >= 0)
    //        {
    //            Instantiate(Resources.Load("MovementMarker"), grid.row[cursorMove.GetCurrentRow() - i].column[cursorMove.GetCurrentCol()].transform.position, Quaternion.identity);
    //        }
    //    }
    //    actionMenu.SetMoveChoosen(false);
    //}
}
