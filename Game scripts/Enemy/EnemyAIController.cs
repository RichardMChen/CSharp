using UnityEngine;
using System.Collections.Generic;

public class EnemyAIController : MonoBehaviour {

    #region Variables

    private bool startEnemyProcedure;
    private bool doBattle;
    //private bool reassign;
    //private bool nextTurn;
    private int enemyCounter;

    private GameController gameController;
    //private GameObject enemyToTakeAction;
    private GameObject enemyCurrentlyTakingTurn;
    private Battle battleControl;
    private Quaternion playerCharOriginalRotation, enemyCharOriginalRotation;
    //GameObject target;

    //public GameObject[] players;
    public List<GameObject> playersList = new List<GameObject>();

    #endregion

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //players = GameObject.FindGameObjectsWithTag("Player");
        AddAllPlayers();
        battleControl = gameObject.GetComponent<Battle>();

        //enemyToTakeAction = null;
        enemyCurrentlyTakingTurn = null;
        doBattle = false;
        enemyCounter = 0;
        //reassign = false;
        //nextTurn = false;
        //AssignTargetToEnemies();
        //target = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("e counter: " + enemyCounter);
        //Debug.Log("enemies waiting: " + CheckIfAllEnemiesWaiting());
        //Debug.Log("taking turn: " + enemyCurrentlyTakingTurn);
        for (int i = 0; i < playersList.Count; i++)
        {
            Debug.Log("playerList: " + playersList[i]);
        }
        Debug.Log("playerList length: " + playersList.Count);

        if (gameController.GetEnemyTurnState() == true)
        {
            //for (int i = 0; i < gameController.enemiesList.Count; i++)
            //{
            //bool moveOn = false;
            //if (nextTurn == true)
            //{
            //    enemyCounter++;
            //    nextTurn = false;
            //}
            if (enemyCounter < gameController.enemiesList.Count)
            {
                enemyCurrentlyTakingTurn = gameController.enemiesList[enemyCounter];
                EnemyMove enemyMove = GameObject.Find(enemyCurrentlyTakingTurn.name).GetComponent<EnemyMove>();

                if (enemyMove.GetHasMovedState() == false && gameController.GetDisplayTurnTextState() == false /*&& enemyMove.GetPerformActionState() == false*/)
                {
                    enemyMove.SetPerformActionsState(true);
                }
                else
                {
                    enemyMove.SetPerformActionsState(false);
                }

                if (doBattle == true)
                {
                    //Debug.Log("BATTERU!!");
                    //battleControl.SetEnemyBattleModeState(true);
                    //battleControl.SetPerformEnemyAttack(true);
                    ////EnemyMove enemyMove = GameObject.Find(enemyCurrentlyTakingTurn.name).GetComponent<EnemyMove>();
                    //enemyCharOriginalRotation = enemyCurrentlyTakingTurn.transform.rotation;
                    //playerCharOriginalRotation = GameObject.Find(enemyMove.GetTarget().name).transform.rotation;

                    //enemyCurrentlyTakingTurn.transform.position = GameObject.Find("EnemySpot").transform.position;
                    //enemyCurrentlyTakingTurn.transform.LookAt(GameObject.Find("PlayerSpot").transform.position);

                    //GameObject.Find(enemyMove.GetTarget().name).transform.position = GameObject.Find("PlayerSpot").transform.position;
                    //GameObject.Find(enemyMove.GetTarget().name).transform.LookAt(GameObject.Find("EnemySpot").transform.position);
                    //gameController.SwitchCameras();
                    TransitionToBattleArea();
                    doBattle = false;
                }
            }
            else
            {
                enemyCounter = 0;
            }
        }
        if (gameController.CheckIfNoPlayersLeft() == false)
        {
            gameController.DefeatedStart();
        }
        if (CheckIfAllEnemiesWaiting() == true)
        {
            gameController.PlayerTurnStart();
        }
        //}
    }

    public void AssignTargetToEnemies()
    {
        for (int i = 0; i < gameController.enemiesList.Count; i++)
		{
            EnemyMove enemyMove = GameObject.Find(gameController.enemiesList[i].name).GetComponent<EnemyMove>();
            enemyMove.AssignTarget(playersList[Random.Range(0, playersList.Count)]);
		}
        
    }

    public void TransitionToBattleArea()
    {
        battleControl.SetEnemyBattleModeState(true);
        battleControl.SetPerformEnemyAttack(true);
        EnemyMove enemyMove = GameObject.Find(enemyCurrentlyTakingTurn.name).GetComponent<EnemyMove>();
        enemyCharOriginalRotation = enemyCurrentlyTakingTurn.transform.rotation;
        playerCharOriginalRotation = GameObject.Find(enemyMove.GetTarget().name).transform.rotation;

        enemyCurrentlyTakingTurn.transform.position = GameObject.Find("EnemySpot").transform.position;
        enemyCurrentlyTakingTurn.transform.LookAt(GameObject.Find("PlayerSpot").transform.position);

        GameObject.Find(enemyMove.GetTarget().name).transform.position = GameObject.Find("PlayerSpot").transform.position;
        GameObject.Find(enemyMove.GetTarget().name).transform.LookAt(GameObject.Find("EnemySpot").transform.position);
        gameController.SwitchCameras();
    }

    public void AfterBattle()
    {
        //Debug.Log("EXECUTE AFTER BATTLE STUFF");
        EnemyMove enemyMove = GameObject.Find(enemyCurrentlyTakingTurn.name).GetComponent<EnemyMove>();
        GameObject enemy = GameObject.Find(enemyCurrentlyTakingTurn.name);
        GameObject targetPlayer = GameObject.Find(enemyMove.GetTarget().name);
        CharacterMove playerMove = GameObject.Find(enemyMove.GetTarget().name).GetComponent<CharacterMove>();
        GridWorld gridWorld = GameObject.Find("Grid").GetComponent<GridWorld>();
        CharacterState enemyCharState = enemy.GetComponent<CharacterState>();

        targetPlayer.transform.rotation = playerCharOriginalRotation;
        targetPlayer.transform.position = gridWorld.row[playerMove.GetCurRow()].column[playerMove.GetCurCol()].transform.position;

        enemy.transform.rotation = enemyCharOriginalRotation;
        enemy.transform.position = gridWorld.row[enemyMove.GetCurRow()].column[enemyMove.GetCurCol()].transform.position;
        enemyCharState.SetIsWaiting(true);

        gameController.SwitchCameras();
        enemy.animation.Play("idle");
        //nextTurn = true;
        enemyCounter++;

        CharacterStatus playerStats = GameObject.Find(targetPlayer.name).GetComponent<CharacterStatus>();
        if (playerStats.GetCurrentHealth() <= 0)
        {
            GameObject effect = Instantiate(Resources.Load("SmallFlamesParticles"), gridWorld.row[playerMove.GetCurRow()].column[playerMove.GetCurCol()].transform.position, Quaternion.identity) as GameObject;
            Destroy(effect, 1.5f);
            //Destroy(targetPlayer);
            for (int i = 0; i < playersList.Count; i++)
            {
                if (targetPlayer == playersList[i])
                {
                    Debug.Log(targetPlayer + " same as " + playersList);
                    Destroy(playersList[i]);
                    playersList.Remove(playersList[i]);
                }
            }
            //reassign = true;
        }

        if (gameController.CheckIfNoPlayersLeft() == false)
        {
            gameController.DefeatedStart();
        }
        if (CheckIfAllEnemiesWaiting() == true)
        {
            gameController.PlayerTurnStart();
        }
    }

    public void AddAllPlayers()
    {
        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerGameObjects)
        {
            playersList.Add(player);
        }
    }

    public void SetDoBattle(bool sdb)
    {
        doBattle = sdb;
    }

    public GameObject GetEnemyCurrentlyTakingTurn()
    {
        return enemyCurrentlyTakingTurn;
    }

    public void SetNextTurn()
    {
        //nextTurn = snt;
        enemyCounter++;
    }

    public bool CheckIfAllEnemiesWaiting()
    {
        bool allEnemiesWaiting = false;
        int counter = 0;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyList.Length; i++)
        {
            CharacterState charState = enemyList[i].GetComponent<CharacterState>();
            if (charState.GetIsWaiting() == false)
            {
                break;
            }
            else
            {
                counter++;
            }
        }

        if (counter == enemyList.Length)
        {
            allEnemiesWaiting = true;
        }
        return allEnemiesWaiting;
    }

    public void ResetEnemyCounter()
    {
        enemyCounter = 0;
    }    
}
