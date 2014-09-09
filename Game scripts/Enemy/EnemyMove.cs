using UnityEngine;
using System.Collections.Generic;

public class EnemyPosition
{
    public int row;  // The row of the marker
    public int col;  // The col of the marker
}

public class EnemyMove : MonoBehaviour 
{

    //private GridWorld grid;
    //private CursorMovement cursorMove;  // Reference the cursorMove script in order to know where to place the movement markers
    //private CursorSelection cursorSelection;  // Reference the cursorSelection script in order to determine when cursor is in move mode
    //private CharacterStatus charStats;  // Reference the CharacterStatus script to know what the movement range for the character selected to move
    ////private ActionMenu actionMenu;  // References the ActionMenu script
    //private MoveConfirmMenu moveConfirmMenu; // Reference the MoveConfirmationMenu script
    //private GridTile gridTile;
    //private GameController gameController;
    //private bool showMoveCounterText;
    //private bool performMovement;
    //public bool hasMoved;
    ////private bool hitLastMarker;
    ////private bool showMoveConfirmationMenu;
    //private int moveCounter;   // Stores the movement amount for the current character

    //// Temperarily public for debugging
    //public int prevRow;  // Used to keep track of the previous 
    //public int prevCol;

    //public List<Position> pathList = new List<Position>();  // A path list to hold the positions(row, col) of the path markers
    //public List<GameObject> pathListGameObjects = new List<GameObject>();  // A path list to hold the path marker gameobjects

    private GameObject target;
    //private GameController gameController;
    private EnemyAIController enemyAI;
    private CharacterStatus charStat;
    private GridWorld gridWorld;
    private GameController gameController;
    //private Battle battleController;

    private int moveCounter;
    private int movePathCounter;
    //private bool endTurn;
    private bool performMovement;
    private bool hasMoved;
    private bool performActions;
    private bool finishedAction;
    private bool targetFound;
    private bool reassign;

    public int startRow;  // Starting row that the character starts at when the map starts
    public int startCol;  // Starting column that the character starts at when the map starts
    public int curRow;  // Current row that the character is at
    public int curCol;  // Current row that the character is at

    public List<EnemyPosition> enemyPathList = new List<EnemyPosition>();
    public float moveSpeed;
    //int movePathCounter;

	// Use this for initialization
	void Start () 
    {
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
        enemyAI = GameObject.Find("GameController").GetComponent<EnemyAIController>();
        charStat = gameObject.GetComponent<CharacterStatus>();
        gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //battleController = GameObject.Find("GameController").GetComponent<Battle>();

        curRow = startRow;
        curCol = startCol;
        moveCounter = charStat.GetMovementRange();
        target = null;
        //endTurn = false;
        performActions = false;
        hasMoved = false;
        finishedAction = false;
        targetFound = false;
        //prevRow = curRow;
        //prevCol = curCol;
        //grid = GameObject.Find("Grid").GetComponent<GridWorld>();
        //cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        //cursorSelection = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        //charStats = this.GetComponent<CharacterStatus>();
        //moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //gridTile = null;
        //moveCounter = charStats.GetMovementRange();
        //showMoveCounterText = false;
        performMovement = false;
        //hasMoved = false;
        movePathCounter = 0;
        reassign = false;

        transform.position = gridWorld.row[startRow].column[startCol].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(gameObject.name + " moved? " + hasMoved);
        //Debug.Log("target found? " + targetFound);
        //Debug.Log("from ai: " + enemyAI.GetEnemyCurrentlyTakingTurn());
        //Debug.Log("is near: " + CheckIfPlayerCharacterNear());

        if (performActions == true && gameController.GetEnemyTurnState() == true && enemyAI.GetEnemyCurrentlyTakingTurn().name == gameObject.name && gameController.CheckIfNoPlayersLeft() == false)
        {
            if (performMovement == false)
            {
                MoveToTarget();
            }
            else if (performMovement == true)
            {
                EnemyMoving();
            }

            if (hasMoved == true && targetFound == true)
            {
                enemyAI.SetDoBattle(true);
            }
            else if(hasMoved == true && targetFound == false)
            {
                CharacterState enemyCharState = gameObject.GetComponent<CharacterState>();
                enemyCharState.SetIsWaiting(true);
                enemyAI.SetNextTurn();
            }
        }

        //if(performMovement == true)
        //{
        //    EnemyMoving();
        //}

        //for (int i = 0; i < enemyPathList.Count; i++)
        //{
        //    Debug.Log("row: " + enemyPathList[i].row + " col: " + enemyPathList[i].col);			 
        //}
    }

    public void MoveToTarget()
    {
        enemyPathList.Clear();
        for (int i = 0; i < enemyAI.playersList.Count; i++)
        {
            if (target == enemyAI.playersList[i])
            {
                reassign = false;
                break;
            }
            else
            {
                reassign = true;
            }
        }

        if(reassign == true)
        {
            target = enemyAI.playersList[Random.Range(0, enemyAI.playersList.Count)];
        }
        CharacterMove targetMovement = GameObject.Find(target.name).GetComponent<CharacterMove>();
        moveCounter = charStat.GetMovementRange();
        movePathCounter = 0;
        targetFound = false;
        int nextRow = curRow;
        int nextCol = curCol;
        
        while (performMovement == false)
        {
            if (enemyPathList.Count == 0)
            {
                EnemyPosition firstPos = new EnemyPosition();
                firstPos.row = curRow;
                firstPos.col = curCol;
                enemyPathList.Add(firstPos);
            }
            
            if (CheckIfPlayerCharacterNear(nextRow, nextCol) == true)
            {
                targetFound = true;
                performMovement = true;
            }
            else if (moveCounter <= 0)
            {
                performMovement = true;
            }
            else
            {
                if (targetMovement.GetCurCol() < nextCol)
                {
                    EnemyPosition ep = new EnemyPosition();
                    ep.row = nextRow;
                    ep.col = nextCol - 1;
                    nextCol -= 1;
                    enemyPathList.Add(ep);
                    moveCounter--;
                }
                else if (targetMovement.GetCurCol() > nextCol)
                {
                    EnemyPosition ep = new EnemyPosition();
                    ep.row = nextRow;
                    ep.col = nextCol + 1;
                    nextCol += 1;
                    enemyPathList.Add(ep);
                    moveCounter--;
                }
                else if(targetMovement.GetCurRow() < nextRow)
                {
                    EnemyPosition ep = new EnemyPosition();
                    ep.row = nextRow - 1;
                    ep.col = nextCol;
                    nextRow -= 1;
                    enemyPathList.Add(ep);
                    moveCounter--;
                }
                else if (targetMovement.GetCurRow() > nextRow)
                {
                    EnemyPosition ep = new EnemyPosition();
                    ep.row = nextRow + 1;
                    ep.col = nextCol;
                    nextRow += 1;
                    enemyPathList.Add(ep);
                    moveCounter--;
                }
            }
        }
    }

    public void AssignTarget(GameObject tar)
    {
        target = tar;
    }

    public bool CheckIfPlayerCharacterNear(int theRow, int theCol)
    {
        bool isNear = false;
        for (int i = 0; i < enemyAI.playersList.Count; i++)
        {
            CharacterMove playerMove = GameObject.Find(enemyAI.playersList[i].name).GetComponent<CharacterMove>();
            /* Check up */
            if (playerMove.GetCurCol() == theCol && playerMove.GetCurRow() - 1 == theRow)
            {
                isNear = true;
                target = enemyAI.playersList[i];
                break;
            }
            /* Check down */
            else if (playerMove.GetCurCol() == theCol && playerMove.GetCurRow() + 1 == theRow)
            {
                isNear = true;
                target = enemyAI.playersList[i];
                break;
            }
            /* Check left */
            else if(playerMove.GetCurCol() - 1 == theCol && playerMove.GetCurRow() == theRow)
            {
                isNear = true;
                target = enemyAI.playersList[i];
                break;
            }
            /* Check right */
            else if (playerMove.GetCurCol() + 1 == theCol && playerMove.GetCurRow() == theRow)
            {
                isNear = true;
                target = enemyAI.playersList[i];
                break;
            }
            else
            {
                isNear = false;
            }
        }
        return isNear;
    }

    public void EnemyMoving()
    {
        if(movePathCounter < enemyPathList.Count)
        {

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                            gridWorld.row[enemyPathList[movePathCounter].row].column[enemyPathList[movePathCounter].col].transform.position,
                                            moveSpeed * Time.deltaTime);
            if (gameObject.transform.position == gridWorld.row[enemyPathList[movePathCounter].row].column[enemyPathList[movePathCounter].col].transform.position)
            {
                movePathCounter++;
            }
        }
        else if (movePathCounter == enemyPathList.Count)
        {
            hasMoved = true;
            performMovement = false;
            CharacterState enemyCharState = gameObject.GetComponent<CharacterState>();
            enemyCharState.SetIsWaiting(false);
            //performActions = true;
            //performActions = true;
            curRow = enemyPathList[enemyPathList.Count - 1].row;
            curCol = enemyPathList[enemyPathList.Count - 1].col;
        }
    }

    public bool OccupiedNearby(CharacterMove tarMove, GameObject theEnemy, string direction)
    {
        bool canUse = true;
        for (int i = 0; i < gameController.enemiesList.Count; i++)
		{
            EnemyMove enemyMove = GameObject.Find(enemyAI.GetEnemyCurrentlyTakingTurn().name).GetComponent<EnemyMove>();
            if(theEnemy == gameController.enemiesList[i])
            {
                continue;
            }

            if (direction == "up")
            {
                if (tarMove.GetCurRow() - 1 == enemyMove.GetCurRow() && tarMove.GetCurCol() == enemyMove.GetCurCol())
                {
                    canUse = false;
                }
            }
            else if (direction == "down")
            {
                if (tarMove.GetCurRow() + 1 == enemyMove.GetCurRow() && tarMove.GetCurCol() == enemyMove.GetCurCol())
                {
                    canUse = false;
                }
            }
            else if (direction == "left")
            {
                if (tarMove.GetCurRow() == enemyMove.GetCurRow() && tarMove.GetCurCol() - 1 == enemyMove.GetCurCol())
                {
                    canUse = false;
                }
            }
            else if (direction == "right")
            {
                if (tarMove.GetCurRow() == enemyMove.GetCurRow() && tarMove.GetCurCol() + 1 == enemyMove.GetCurCol())
                {
                    canUse = false;
                }
            }
		}     
        return canUse;
    }

    public int GetCurRow()
    {
        return curRow;
    }

    public int GetCurCol()
    {
        return curCol;
    }

    public bool GetFinishedActionState()
    {
        return finishedAction;
    }

    //public bool GetPerformMovementState()
    //{
    //    return performMovement;
    //}

    public void SetHasMovedState(bool shms)
    {
        hasMoved = shms;
    }

    public bool GetHasMovedState()
    {
        return hasMoved;
    }

    public void SetPerformActionsState(bool spas)
    {
        performActions = spas;
    }

    public bool GetPerformActionState()
    {
        return performActions;
    }

    public GameObject GetTarget()
    {
        return target;
    }

}
