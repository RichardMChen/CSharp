using UnityEngine;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{

#region Variables

    private bool levelStart;
    private bool playerTurn;  // A bool to tell if it is the player's turn
    private bool enemyTurn;  // A bool to tell if it is the enemy's turn
    private bool attackMode;  // A bool to tell if a player character is displaying their attack range and the mode to determine if the player initiates an attack
    private bool charIsMoving;  // A bool to check if the player is moving along it's movement path
    private bool displayAttackRange;
    private bool displayTurnText;  // A bool for the text that displays at the beginning of the player and enemies turn
    private bool victory;
    private bool defeat;
    //private bool playerTurnStart;
    //private bool enemyTurnStart;
    private string previousMenu;
    private GameObject enemySelected;
    private Battle battle;
    private EnemyAIController enemyAIController;
    private ActionMenu actionMenu;
    private MoveConfirmMenu moveConfirmMenu;
    private Quaternion playerCharOriginalRotation, enemyCharOriginalRotation;

    private int displayTurnTimer;
    private const int displayTurnTextConst = 4;

    //CursorSelection cursorSelection;
    public Camera mapCam;
    public Camera battleCam;
    public AudioClip mapMusic;
    public AudioClip battleMusic;
    //GameObject[] players;

    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> enemiesToBeRemoved = new List<GameObject>();

#endregion

    // Use this for initialization
    void Start()
    {
        battle = gameObject.GetComponent<Battle>();
        enemyAIController = gameObject.GetComponent<EnemyAIController>();
        actionMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();
        moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();

        levelStart = true;
        playerTurn = false;
        enemyTurn = false;
        attackMode = false;
        charIsMoving = false;
        displayAttackRange = false;
        previousMenu = "";
        enemySelected = null;
        AddAllEnemies();
        AddAllPlayers();
        mapCam.enabled = true;
        battleCam.enabled = false;
        victory = false;
        defeat = false;
        //displayTurnText = true;
        //playerTurnStart = false;
        //enemyTurnStart = false;
        displayTurnTimer = displayTurnTextConst;
        audio.loop = true;

        //players = GameObject.FindGameObjectsWithTag("Player");
        //cursorSelection = GameObject.Find("Cursor").GetComponent<CursorSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("e count: " + enemiesList.Count);
        //Debug.Log("charStates length: " + charStates.Length);
        //Debug.Log("Selected enemy: " + enemySelected);
        //for (int i = 0; i < players.Length; i++)
        //{
        //    Debug.Log("player list: " + players[i]);
        //}
        //if(attackMode == true)
        //{
        //    CursorIsOnAttackMarker();
        //}
#region Debugging
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    PlayerTurnStart();
        //}
        //Debug.Log("player turn " + playerTurn);
        //Debug.Log("enemy turn " + enemyTurn);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //CheckAllPlayersMoved();
        //    Debug.Log("all players moved: " + CheckAllPlayersMoved());
        //}

        /* Debugging the cameras */
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    mapCam.enabled = !mapCam.enabled;
        //    battleCam.enabled = !battleCam.enabled;
        //}

        //for (int i = 0; i < enemiesList.Count; i++)
        //{
        //    Debug.Log("enemies list: " + i + " "+ enemiesList[i]);
        //}
        Debug.Log("defeat: " + defeat);
        Debug.Log("players left? " + CheckIfNoPlayersLeft());
#endregion

        if (CheckIfNoPlayersLeft() == true)
        {
            defeat = true;
            //Debug.Log("HI!");
        }
        else
        {
            if (levelStart == true)
            {
                PlayerTurnStart();
                enemyAIController.AssignTargetToEnemies();
                levelStart = false;
            }

            if (displayTurnText == true || enemyTurn == true)
            {
                GameObject cursorObject = GameObject.Find("Cursor");
                cursorObject.renderer.enabled = false;
            }
            else
            {
                GameObject cursorObject = GameObject.Find("Cursor");
                cursorObject.renderer.enabled = true;
            }

            if(actionMenu.getWaitChoosen() == true)
            {
                if(CheckAllPlayersWaiting() == true)
                {
                    EnemyTurnStart();
                }
            }

            if(moveConfirmMenu.GetWaitChosenState() == true)
            {
                if(CheckAllPlayersWaiting() == true)
                {
                    EnemyTurnStart();
                }
            }

            if (battle.GetBattleModeState() == false && displayTurnText == false)
            {
                if (Input.GetButtonDown("Confirm"))
                {
                    if (attackMode == true)
                    {
                        if (CursorIsOnEnemy() == true && CursorIsOnAttackMarker() == true)
                        {
                            battle.SetBattleModeState(true);
                            CursorSelection cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
                            enemyCharOriginalRotation = enemySelected.transform.rotation;
                            playerCharOriginalRotation = GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).transform.rotation;
                            enemySelected.transform.position = GameObject.Find("EnemySpot").transform.position;
                            enemySelected.transform.LookAt(GameObject.Find("PlayerSpot").transform.position);
                            GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).transform.position = GameObject.Find("PlayerSpot").transform.position;
                            GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).transform.LookAt(GameObject.Find("EnemySpot").transform.position);
                            cursorSelect.SetMoveModeState(false);
                            if (audio.isPlaying)
                            {
                                audio.Stop();
                            }
                            audio.clip = battleMusic;
                            audio.Play();
                            audio.loop = true;
                            SwitchCameras();
                        }
                    }
                }
                else if (Input.GetButtonDown("Cancel"))
                {
                    //PlayerAttack playerAttack = GameObject.Find(cursorSelection.GetSelectedPlayerCharName()).GetComponent<PlayerAttack>();
                    //playerAttack.SetCalculateAttackRange(false);
                    if (attackMode == true)
                    {
                        //MoveConfirmMenu moveConfirmMenu = GameObject.Find("Main Camera").GetComponent<MoveConfirmMenu>();
                        CursorSelection cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
                        CursorMovement cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
                        GridWorld gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
                        CharacterMove charMove = GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<CharacterMove>();
                        PlayerAttack playerAttack = GameObject.Find(cursorSelect.GetSelectedPlayerCharName()).GetComponent<PlayerAttack>();
                        GameObject.Find("Cursor").transform.position = gridWorld.row[charMove.GetCurRow()].column[charMove.GetCurCol()].transform.position;
                        GameObject[] attackMarkers = GameObject.FindGameObjectsWithTag("AttackMarker");
                        for (int i = 0; i < attackMarkers.Length; i++)
                        {
                            Destroy(attackMarkers[i]);
                        }
                        playerAttack.ClearAttackMarkerList();
                        attackMode = false;
                        displayAttackRange = false;

                        if (previousMenu == "action")
                        {
                            ActionMenu actMenu = GameObject.Find("Main Camera").GetComponent<ActionMenu>();
                            cursorMove.SetCurrentRow(charMove.GetCurRow());
                            cursorMove.SetCurrentCol(charMove.GetCurCol());
                            actMenu.SetIsActionMenuShow(true);
                        }
                        else if (previousMenu == "move")
                        {
                            moveConfirmMenu.SetMoveConfirmationMenu(true);
                        }
                        else if (previousMenu == "afterMove")
                        {

                            cursorMove.SetCurrentRow(charMove.GetCurRow());
                            cursorMove.SetCurrentCol(charMove.GetCurCol());
                            moveConfirmMenu.SetShowAfterMoveMenu(true);
                        }
                    }
                }
            }

            //Debug.Log("is moving: " + charIsMoving);
            //Debug.Log("attack mode: " + attackMode + ", display range: " + displayAttackRange);
            //Debug.Log("attack mode: " + attackMode);
            //Debug.Log("Prev menu: " + previousMenu);
        }
    }

    void OnGUI()
    {        
        if (victory == true)
        {
            GUIStyle victoryGuiStyle = new GUIStyle(GUI.skin.button);
            GUI.color = Color.cyan;
            victoryGuiStyle.fontSize = 60;
            GUI.Box(new Rect(Screen.width / 2 - 155, Screen.height / 2 - 150, 350, 150), "VICTORY", victoryGuiStyle);
        }
        else if(defeat == true)
        {
            GUIStyle defeatGUIStyle = new GUIStyle(GUI.skin.button);
            GUI.color = Color.red;
            defeatGUIStyle.fontSize = 60;
            GUI.Box(new Rect(Screen.width / 2 - 155, Screen.height / 2 - 150, 350, 150), "DEFEAT", defeatGUIStyle);
        }
        else if (playerTurn == true && displayTurnText == true && victory == false)
        {
            GUIStyle playerTurnGuiStyle = new GUIStyle(GUI.skin.button);
            GUI.color = Color.blue;
            playerTurnGuiStyle.fontSize = 60;
            GUI.Box(new Rect(Screen.width / 2 - 155, Screen.height / 2 - 150, 350, 150), "Player Turn", playerTurnGuiStyle);
        }
        else if (enemyTurn == true && displayTurnText == true && victory == false)
        {
            GUIStyle enemyTurnGuiStyle = new GUIStyle(GUI.skin.button);
            GUI.color = Color.red;
            enemyTurnGuiStyle.fontSize = 60;
            GUI.Box(new Rect(Screen.width / 2 - 155, Screen.height / 2 - 150, 350, 150), "Enemy Turn", enemyTurnGuiStyle);
        }
    }

    /* Function to run procedures when the player's turn starts */
    public void PlayerTurnStart()
    {
        if (CheckIfNoEnemiesLeft() == true)
        {
            //Debug.Log("VICTORY");
            victory = true;
        }
        else if(CheckIfNoPlayersLeft() == true)
        {
            defeat = true;
        }
        else
        {
            enemyTurn = false;
            playerTurn = true;
            displayTurnText = true;
            //enemyAIController.ResetEnemyCounter();

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  // An array to hold all of the gameobjects tagged "Player" and change their states
            //GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<CharacterState>().SetIsWaiting(false);
            }

            for (int i = 0; i < enemiesList.Count; i++)
            {
                enemiesList[i].GetComponent<CharacterState>().SetIsWaiting(false);
                enemiesList[i].GetComponent<EnemyMove>().SetHasMovedState(false);
                enemiesList[i].GetComponent<EnemyMove>().enemyPathList.Clear();
            }

            InvokeRepeating("TurnTextDisplay", 1, 1);
        }
    }

    /* Function to run procedures when the enemy's turn starts*/
    public void EnemyTurnStart()
    {
        if (CheckIfNoEnemiesLeft() == true)
        {
            Debug.Log("VICTORY");
            victory = true;
        }
        else
        {
            enemyTurn = true;
            playerTurn = false;
            displayTurnText = true;
            enemyAIController.ResetEnemyCounter();

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  // An array to hold all of the gameobjects tagged "Player" and change their states
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<CharacterState>().SetIsWaiting(false);
            }

            for (int i = 0; i < enemiesList.Count; i++)
            {
                enemiesList[i].GetComponent<CharacterState>().SetIsWaiting(false);
                //enemiesList[i].GetComponent<EnemyMove>().SetTargetFound(false);
            }
            InvokeRepeating("TurnTextDisplay", 1, 1);
        }
    }

    public void DefeatedStart()
    {
        //Debug.Log("from defeated start.");
    }

    private void TurnTextDisplay()
    {
        if(--displayTurnTimer == 0)
        {
            displayTurnText = false;
            GameObject cursorObject = GameObject.Find("Cursor");
            cursorObject.renderer.enabled = true;
            CancelInvoke("TurnTextDisplay");
            displayTurnTimer = displayTurnTextConst;
        }
    }

    public void AddAllEnemies()
    {
        GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyGameObjects)
        {
            enemiesList.Add(enemy);
        }
    }

    public void AddAllPlayers()
    {
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in playerGameObjects)
        {
            playerList.Add(player);
        }
    }

    public bool CursorIsOnEnemy()
    {
        bool isOnEnemy = false;
        CursorMovement cursorMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();

        for (int i = 0; i < enemiesList.Count; i++)
        {
            EnemyMove em = enemiesList[i].GetComponent<EnemyMove>();
            if (cursorMove.GetCurrentRow() == em.GetCurRow())
            {
                if (cursorMove.GetCurrentCol() == em.GetCurCol())
                {
                    isOnEnemy = true;
                    enemySelected = enemiesList[i];
                }
            }
        }
        return isOnEnemy;
    }

    public bool CursorIsOnAttackMarker()
    {
        bool isOnAttMarker = false;
        CursorMovement curMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        CursorSelection curSel = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        PlayerAttack playerAttack = GameObject.Find(curSel.GetSelectedPlayerCharName()).GetComponent<PlayerAttack>();

        for (int i = 0; i < playerAttack.attMarkPosList.Count; i++)
        {
            if (curMove.GetCurrentRow() == playerAttack.attMarkPosList[i].row)
            {
                if (curMove.GetCurrentCol() == playerAttack.attMarkPosList[i].col)
                {
                    isOnAttMarker = true;
                }
            }
        }
        return isOnAttMarker;
    }

    public GameObject GetSelectedEnemy()
    {
        return enemySelected;
    }

    public void TransitionAfterBattle()
    {        
        CursorSelection cursorSelect = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        GameObject player = GameObject.Find(cursorSelect.GetSelectedPlayerCharName());
        //CharacterStatus playerCharStat = player.GetComponent<CharacterStatus>();
        CharacterState playerState = player.GetComponent<CharacterState>();
        CharacterMove playerMove = player.GetComponent<CharacterMove>();
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        GridWorld gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();

        GameObject[] moveMarkers = GameObject.FindGameObjectsWithTag("MoveMarker");
        for (int i = 0; i < moveMarkers.Length; i++)
        {
            Destroy(moveMarkers[i]);
        }
        playerMove.ClearPathList();

        GameObject[] attackMarkers = GameObject.FindGameObjectsWithTag("AttackMarker");
        for (int i = 0; i < attackMarkers.Length; i++)
        {
            Destroy(attackMarkers[i]);
        }
        playerAttack.attMarkPosList.Clear();

        player.transform.rotation = playerCharOriginalRotation;
        player.transform.position = gridWorld.row[playerMove.GetCurRow()].column[playerMove.GetCurCol()].transform.position;        
        playerState.SetIsWaiting(true);
        

        EnemyMove enemyMove = enemySelected.GetComponent<EnemyMove>();
        enemySelected.transform.rotation = enemyCharOriginalRotation;
        enemySelected.transform.position = gridWorld.row[enemyMove.GetCurRow()].column[enemyMove.GetCurCol()].transform.position;

        attackMode = false;
        cursorSelect.SetIsSelected(false);
        SwitchCameras();

        if(audio.isPlaying)
        {
            audio.Stop();
        }
        audio.clip = mapMusic;
        audio.Play();
        audio.loop = true;

        CharacterStatus enemyCharStatus = GameObject.Find(enemySelected.name).GetComponent<CharacterStatus>();
        if (enemyCharStatus.GetCurrentHealth() <= 0)
        {
            GameObject effect = Instantiate(Resources.Load("SmallFlamesParticles"), gridWorld.row[enemyMove.GetCurRow()].column[enemyMove.GetCurCol()].transform.position, Quaternion.identity) as GameObject;
            Destroy(effect, 1.5f);
            enemiesToBeRemoved.Add(enemySelected);
            //enemySelected.renderer.enabled = false;
        }

        //Debug.Log("all players moved: " + CheckAllPlayersWaiting());
        //if(CheckAllPlayersWaiting() == true)
        //{
            if(enemiesToBeRemoved.Count > 0)
            {
                for (int i = 0; i < enemiesList.Count; i++)
                {
                    for (int j = 0; j < enemiesToBeRemoved.Count; j++)
                    {
                        if(enemiesList[i] == enemiesToBeRemoved[j])
                        {
                            Debug.Log(enemiesList[i] + " same as " + enemiesToBeRemoved[j]);
                            Destroy(enemiesList[i]);
                            enemiesList.Remove(enemiesList[i]);
                        }
                    }
                }
                enemiesToBeRemoved.Clear();
            }
        //}
        if (CheckIfNoEnemiesLeft() == false && CheckAllPlayersWaiting() == true)
        {
            EnemyTurnStart();
        }
    }

    /* Function to check if all the player characters are waiting */
    private bool CheckAllPlayersWaiting()
    {
        bool allPlayersMoved = false;
        int counter = 0;
        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersList.Length; i++)
        {
            CharacterState charState = playersList[i].GetComponent<CharacterState>();
            if (charState.GetIsWaiting() == false)
            {
                break;
            }
            else
            {
                counter++;
            }
        }

        if (counter == playersList.Length)
        {
            allPlayersMoved = true;
        }
        return allPlayersMoved;
    }

    public void SwitchCameras()
    {
        mapCam.enabled = !mapCam.enabled;
        battleCam.enabled = !battleCam.enabled;
    }

    public bool CheckIfNoEnemiesLeft()
    {
        bool anyLeft = false;
        GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemyGameObjects.Length == 0)
        {
            anyLeft = true;
        }
        return anyLeft;
    }

    public bool CheckIfNoPlayersLeft()
    {
        bool playersLeft = false;
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        if(playerGameObjects.Length == 0)
        {
            playersLeft = true;
        }
        return playersLeft;
    }

    public bool GetDefeatState()
    {
        return defeat;
    }

#region Setters and Getters

    /* Sets the bool to tell if a "Player" character is moving or not */
    public void SetCharIsMoving(bool scim)
    {
        charIsMoving = scim;
    }

    /* Gets the bool to tell if a "Player" character is moving or not */
    public bool GetCharIsMoving()
    {
        return charIsMoving;
    }

    /* Function to set all of the "Player" gameobjects to wait */
    public void SetPlayerCharsToWait()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<CharacterState>().SetIsWaiting(true);
        }
    }

    public bool GetAttackModeState()
    {
        return attackMode;
    }

    public void SetAttackModeState(bool sams)
    {
        attackMode = sams;
    }

    public bool GetDisplayAttackRangeState()
    {
        return displayAttackRange;
    }

    public void SetDisplayAttackRangeState(bool sdars)
    {
        displayAttackRange = sdars;
    }

    public void SetPreviousMenu(string prevM)
    {
        previousMenu = prevM;
    }

    public string GetPreviousMenu()
    {
        return previousMenu;
    }

    public bool GetDisplayTurnTextState()
    {
        return displayTurnText;
    }

    public bool GetPlayerTurnState()
    {
        return playerTurn;
    }

    public bool GetEnemyTurnState()
    {
        return enemyTurn;
    }

#endregion
}
