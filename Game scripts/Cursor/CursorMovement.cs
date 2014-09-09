/* Handles the cursor movement */

using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {

    private int prevRow;    // Stores the previous row that the cursor was on when it selected a character
    private int prevCol;    // Stores the previous column that the cursor was on when it selected a character

    private Vector3 currentLocation; // The current location of the cursor in terms of [rows, columns]
    private int currentRow;  // The current row that the cursor is on
    private int currentCol;  // The current column that the cursor is on
    //public float cursorSpeed = 2;   // How fast the cursor moves

    private GridWorld grid;   // Reference to the GridWorld script 
    //private CursorSelection cursorSelection;   // Reference to the CursorSelection script
    private ActionMenu actionMenu;   // Reference to the ActionMenu script
    private MoveConfirmMenu moveConfirmMenu;  // Reference the MoveConfirmationMenu script
    private EmptyTileMenu emptytileMenu;
    private GameController gameController;
    private Battle battle;
    private Vector3 cursorStartPosition;   // The starting position of the cursor
    //private Vector3 cursorStartPositionCol;

    public int startRow;    // The row that the cursor starts on the grid
    public int startCol;    // The column that the cursor starts on the grid

    void Awake()
    {
        actionMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();
        grid = GameObject.Find("Grid").GetComponent<GridWorld>();
        moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
        emptytileMenu = GameObject.Find("Main Camera").GetComponent<EmptyTileMenu>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        battle = GameObject.Find("GameController").GetComponent<Battle>();
        //cursorSelection = gameObject.GetComponent<CursorSelection>();
    }

    void Start()
    {
        /* Initialize the starting position of the cursor to the startRow and startCol integer values
           and keeping the same y-position from the scene view */
        cursorStartPosition.x = grid.row[startRow].column[startCol].transform.localPosition.x;
        cursorStartPosition.y = transform.localPosition.y;
        cursorStartPosition.z = grid.row[startRow].column[startCol].transform.localPosition.z;

        /* Sets the cursor's local position to the start position specified above */
        transform.localPosition = cursorStartPosition;


        /* Initializes the current row and current column to the starting row and starting column */
        currentRow = startRow;
        currentCol = startCol;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("prev row: " + prevRow + " prev col:" + prevCol);
        //for (int i = 0; i < grid.row.Length; i++)
        //{
        //    for (int j = 0; j < grid.row[i].column.Length; j++)
        //    {
        //        Debug.Log(grid.row[i].column[j].name + ": " + grid.row[i].column[j].transform.localPosition);
        //    }
        //}
        //Debug.Log(grid.row[1].column.Length);
        //Debug.Log("Current Location: [" + currentRow + ", " + currentCol + "]");

        if (battle.GetBattleModeState() == false && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true && gameController.GetDefeatState() == false)
        {
            /* If up key is pressed move up */
            if (Input.GetButtonDown("Up") && actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                moveConfirmMenu.GetShowAfterMoveMenu() == false && emptytileMenu.GetIsEmptyTileMenuShow() == false && gameController.GetCharIsMoving() == false)
            {
                MoveUp();
            }

            /* If down key is pressed move down */
            if (Input.GetButtonDown("Down") && actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                moveConfirmMenu.GetShowAfterMoveMenu() == false && emptytileMenu.GetIsEmptyTileMenuShow() == false && gameController.GetCharIsMoving() == false)
            {
                MoveDown();
            }

            /* If left key is pressed move left */
            if (Input.GetButtonDown("Left") && actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                moveConfirmMenu.GetShowAfterMoveMenu() == false && emptytileMenu.GetIsEmptyTileMenuShow() == false && gameController.GetCharIsMoving() == false)
            {
                MoveLeft();
            }

            /* If right key is pressed move right */
            if (Input.GetButtonDown("Right") && actionMenu.GetIsActionMenuShow() == false && moveConfirmMenu.GetMoveConfirmationMenu() == false &&
                moveConfirmMenu.GetShowAfterMoveMenu() == false && emptytileMenu.GetIsEmptyTileMenuShow() == false && gameController.GetCharIsMoving() == false)
            {
                MoveRight();
            }
        }
	}

#region Directional_Movement
    /* Checks to see if current row is within bounds of the 2D array. Move up */
    void MoveUp()
    {
        if(currentRow - 1 >= 0)
        {
            currentLocation.x = grid.row[currentRow - 1].column[currentCol].transform.localPosition.x;
            currentLocation.y = transform.localPosition.y;
            currentLocation.z = grid.row[currentRow - 1].column[currentCol].transform.localPosition.z;
            transform.localPosition = currentLocation;
            currentRow--;
        }
    }

    /* Checks to see if current row is within bounds of the 2D array. Move down */
    void MoveDown()
    {
        if (currentRow + 1 < grid.row.Length)
        {
            currentLocation.x = grid.row[currentRow + 1].column[currentCol].transform.localPosition.x;
            currentLocation.y = transform.localPosition.y;
            currentLocation.z = grid.row[currentRow + 1].column[currentCol].transform.localPosition.z;
            transform.localPosition = currentLocation;
            currentRow++;
        }
    }

    /* Checks to see if current column is within bounds of the 2D array. Move left */
    void MoveLeft()
    {
        if (currentCol - 1 >= 0)
        {
            currentLocation.x = grid.row[currentRow].column[currentCol - 1].transform.localPosition.x;
            currentLocation.y = transform.localPosition.y;
            currentLocation.z = grid.row[currentRow].column[currentCol - 1].transform.localPosition.z;
            transform.localPosition = currentLocation;
            currentCol--;
        }
    }

    /* Checks to see if current column is within bounds of the 2D array. Move right */
    void MoveRight()
    {
        if (currentCol + 1 < grid.row[currentRow].column.Length)
        {
            currentLocation.x = grid.row[currentRow].column[currentCol + 1].transform.localPosition.x;
            currentLocation.y = transform.localPosition.y;
            currentLocation.z = grid.row[currentRow].column[currentCol + 1].transform.localPosition.z;
            transform.localPosition = currentLocation;
            currentCol++;
        }
    }
#endregion

#region Setters and Getters
    /* Set the previous row to the current location of the cursor when called. */
    public void SetPrevRow(int r)
    {
        prevRow = r;
    }

    /* Get the previous row that the cursor was on. */
    public int GetPrevRow()
    {
        return prevRow;
    }

    /* Set the previous column to the current location of the cursor when called. */
    public void SetPrevCol(int c)
    {
        prevCol = c;
    }

    /* Get the previous column that the cursor was on. */
    public int GetPrevCol()
    {
        return prevCol;
    }

    /* Sets the current row of the cursor */
    public void SetCurrentRow(int cr)
    {
        currentRow = cr;
    }

    /* Gets the current row that the cursor is currently on */
    public int GetCurrentRow()
    {
        return currentRow;
    }

    /* Sets the current column of the cursor */
    public void SetCurrentCol(int cc)
    {
        currentCol = cc;
    }

    /* Gets the current column that the cursor is currently on */
    public int GetCurrentCol()
    {
        return currentCol;
    }
#endregion
}
