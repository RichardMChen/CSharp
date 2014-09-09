/*
 * This script displays the attack range of the currently selected character and
 * then activates the
 */

using UnityEngine;
using System.Collections.Generic;

public class AttackMarkerPosition
{
    public int row;
    public int col;
}

public class PlayerAttack : MonoBehaviour 
{
    private string selectedTargetName;
    private bool calculateAttackRange;
    //private GameController gameController;
    private CursorSelection cursorSelection;  // Reference the cursorSelection script in order to determine when cursor is in move mode
    private GridWorld grid;
    //private GridTile gridTile;
    private CharacterStatus charStatus;
    private CharacterMove charMove;
    private GameController gameController;

    public List<AttackMarkerPosition> attMarkPosList = new List<AttackMarkerPosition>();

	// Use this for initialization
	void Start () 
    {
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
        cursorSelection = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        grid = GameObject.Find("Grid").GetComponent<GridWorld>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //gridTile = null;
        charStatus = null;
        charMove = null;
        calculateAttackRange = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        //if(cursorSelection.GetSelectedPlayerCharName() == gameObject.name)
        //{
        //    DebugAttMarPosList();
        //}
        //Debug.Log("calculateAttackRange: " + calculateAttackRange);
        if (gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true)
        {
            if (cursorSelection.GetSelectedPlayerCharName() == gameObject.name)
            {
                //Debug.Log(attMarkPosList.Count);
                if (calculateAttackRange == true)
                {
                    charStatus = GameObject.Find(cursorSelection.GetSelectedPlayerCharName()).GetComponent<CharacterStatus>();
                    charMove = GameObject.Find(cursorSelection.GetSelectedPlayerCharName()).GetComponent<CharacterMove>();
                    int attackRange = charStatus.GetAttackRange();

                    for (int i = 1; i <= attackRange; i++)
                    {
                        if (charMove.GetCurRow() - i >= 0)
                        {
                            Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() - i].column[charMove.GetCurCol()].transform.position, Quaternion.identity);
                            AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                            attMarPos.row = charMove.GetCurRow() - i;
                            attMarPos.col = charMove.GetCurCol();
                            attMarkPosList.Add(attMarPos);
                        }

                        if (charMove.GetCurRow() + i < grid.row.Length)
                        {
                            Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() + i].column[charMove.GetCurCol()].transform.position, Quaternion.identity);
                            AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                            attMarPos.row = charMove.GetCurRow() + i;
                            attMarPos.col = charMove.GetCurCol();
                            attMarkPosList.Add(attMarPos);
                        }

                        if (charMove.GetCurCol() - i >= 0)
                        {
                            Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow()].column[charMove.GetCurCol() - i].transform.position, Quaternion.identity);
                            AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                            attMarPos.row = charMove.GetCurRow();
                            attMarPos.col = charMove.GetCurCol() - i;
                            attMarkPosList.Add(attMarPos);
                        }

                        if (charMove.GetCurCol() + i < grid.row[charMove.GetCurRow()].column.Length)
                        {
                            Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow()].column[charMove.GetCurCol() + i].transform.position, Quaternion.identity);
                            AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                            attMarPos.row = charMove.GetCurRow();
                            attMarPos.col = charMove.GetCurCol() + i;
                            attMarkPosList.Add(attMarPos);
                        }
                    }

                    int counter = 1;
                    for (int j = 1; j <= attackRange; j++)
                    {
                        while (counter <= attackRange - j)
                        {
                            // Upper left
                            if (charMove.GetCurRow() - counter >= 0 && charMove.GetCurCol() - j > 0)
                            {
                                Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() - counter].column[charMove.GetCurCol() - j].transform.position, Quaternion.identity);
                                AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                                attMarPos.row = charMove.GetCurRow() - counter;
                                attMarPos.col = charMove.GetCurCol() - j;
                                attMarkPosList.Add(attMarPos);
                            }

                            // Lower Left
                            if (charMove.GetCurRow() + counter < grid.row.Length && charMove.GetCurCol() - j > 0)
                            {
                                Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() + counter].column[charMove.GetCurCol() - j].transform.position, Quaternion.identity);
                                AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                                attMarPos.row = charMove.GetCurRow() + counter;
                                attMarPos.col = charMove.GetCurCol() - j;
                                attMarkPosList.Add(attMarPos);
                            }

                            // Upper Right
                            if (charMove.GetCurRow() - counter >= 0 && charMove.GetCurCol() + j < grid.row[charMove.GetCurRow()].column.Length)
                            {
                                Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() - counter].column[charMove.GetCurCol() + j].transform.position, Quaternion.identity);
                                AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                                attMarPos.row = charMove.GetCurRow() - counter;
                                attMarPos.col = charMove.GetCurCol() + j;
                                attMarkPosList.Add(attMarPos);
                            }

                            // Lower Right
                            if (charMove.GetCurRow() + counter < grid.row.Length && charMove.GetCurCol() + j < grid.row[charMove.GetCurRow()].column.Length)
                            {
                                Instantiate(Resources.Load("AttackMarker"), grid.row[charMove.GetCurRow() + counter].column[charMove.GetCurCol() + j].transform.position, Quaternion.identity);
                                AttackMarkerPosition attMarPos = new AttackMarkerPosition();
                                attMarPos.row = charMove.GetCurRow() + counter;
                                attMarPos.col = charMove.GetCurCol() + j;
                                attMarkPosList.Add(attMarPos);
                            }

                            counter++;
                        }
                    }
                    calculateAttackRange = false;
                }
            }
        }
    }

    void DebugAttMarPosList()
    {
        for (int q = 0; q < attMarkPosList.Count; q++)
        {
            Debug.Log("Row: " + attMarkPosList[q].row + " Col: " + attMarkPosList[q].col);
        }
    }

    public void ClearAttackMarkerList()
    {
        attMarkPosList.Clear();
    }

    public void SetSelectedTarget(string targetName)
    {
        selectedTargetName = targetName;
    }

    public string GetSelectedTarget()
    {
        return selectedTargetName;
    }

    public void SetCalculateAttackRange(bool scar)
    {
        calculateAttackRange = scar;
    }
}
