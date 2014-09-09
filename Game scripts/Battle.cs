using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour
{
#region Variables

    private bool battleMode;
    private bool enemyBattleMode;
    private bool showBattleMenuOptions;
    private bool powerChosen, accuracyChosen;
    private bool battleStart, battleEnd;
    private bool displayGOText;
    private bool attackInProgress;
    private bool activitySequenceState;  // A bool to determine if the mini-game/activities are currently happening
    private bool showHitText;
    private bool showMissText;
    private bool transition;
    private bool performEnemyAttack;
    private int batteMenuOptionsIndex;
    private int amountPerPressed;   // The number amount required to increase a stat by a point
    private int numTimesButtonIsPressed;
    private string[] battleMenuOptions = { "Power", "None", "Accuracy" };

    CursorSelection curSel;
    GameController gameController;
    EnemyAIController enemyAIController;

    /* The variables to hold the values for the power and accuracy values of the player character in battle */
    private int powerValue;
    private int accuracyValue;

    private int enemyPowerValue;
    private int enemyAccuracyValue;

    /* Integers for the number of seconds for timers */
    public int textTimer;
    public int buttonMashTimer;
    public int hitMissTextTimer;
    public int delayTimer;

#endregion

    // Use this for initialization
    void Start()
    {
        battleMode = false;
        showBattleMenuOptions = false;
        batteMenuOptionsIndex = 1;
        amountPerPressed = 5;
        powerChosen = false;
        accuracyChosen = false;
        battleStart = false;
        //battleInProgress = false;
        activitySequenceState = false;
        displayGOText = false;
        numTimesButtonIsPressed = 0;
        attackInProgress = false;
        showHitText = false;
        showMissText = false;
        transition = false;
        enemyBattleMode = false;
        performEnemyAttack = false;

        curSel = GameObject.Find("Cursor").GetComponent<CursorSelection>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        enemyAIController = gameObject.GetComponent<EnemyAIController>();
        //isAttacking = false;
        //battleEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("battleMode: " + battleMode);
        //Debug.Log("enemyBattleMode " + enemyBattleMode);
        //Debug.Log("battleEnd " + battleEnd);
        //Debug.Log("battleStart: " + battleStart);
        //Debug.Log("trans: " + transition);
        //Debug.Log("num: " + numTimesButtonIsPressed);
    #region Battle mode for player turn
        if (battleMode == true && gameController.GetDisplayTurnTextState() == false && gameController.GetPlayerTurnState() == true)
        {
            if (showBattleMenuOptions == true)
            {
                CharacterStatus charStat = GameObject.Find(curSel.GetSelectedPlayerCharName()).GetComponent<CharacterStatus>();
                powerValue = charStat.GetPowerStat();
                accuracyValue = charStat.GetAccuracyStat();
                numTimesButtonIsPressed = 0;
                MenuSelecting();
            }
            if (Input.GetButtonDown("Confirm"))
            {
                if (showBattleMenuOptions == true)
                {
                    HandleSelection();
                }
            }

            if (activitySequenceState == true)
            {
                InvokeRepeating("ButtonMashSequenceTimer", 0.1f, 1);
                ButtonMashSequence();
            }
            else if(attackInProgress == true)
            {
                MakeAttack();
            }

            if (battleStart == false)
            {
                showBattleMenuOptions = true;
            }

            if (battleEnd == true)
            {
                InvokeRepeating("TransitionToBattleEnd", 1, 1);
                if (transition == true)
                {
                    CancelInvoke("TransitionToBattleEnd");
                    battleMode = false;
                    battleEnd = false;
                    battleStart = false;
                    
                    gameController.TransitionAfterBattle();
                }
                transition = false;
            }
        }
    #endregion

        else if (enemyBattleMode == true && gameController.GetDisplayTurnTextState() == false && gameController.GetEnemyTurnState() == true)
        {
            CharacterStatus enemyCharStat = GameObject.Find(enemyAIController.GetEnemyCurrentlyTakingTurn().name).GetComponent<CharacterStatus>();
            enemyPowerValue = enemyCharStat.GetPowerStat();
            enemyAccuracyValue = enemyCharStat.GetAccuracyStat();

            if (performEnemyAttack == true)
            {
                EnemyAttack();
            }
            if (battleEnd == true)
            {
                InvokeRepeating("EnemyTransitionToBattleEnd", 1, 1);
                //enemyAIController.AfterBattle();
                if (transition == true)
                {
                    CancelInvoke("EnemyTransitionToBattleEnd");
                    enemyBattleMode = false;
                    battleEnd = false;
                    enemyAIController.AfterBattle();
                }
                transition = false;
            }
        }
        else
        {
            showBattleMenuOptions = false;
            textTimer = 3;
            hitMissTextTimer = 2;
            buttonMashTimer = 300;
        }
    }

    void OnGUI()
    {
        string controlName = gameObject.GetHashCode().ToString();
        GUI.SetNextControlName(controlName);
        Rect bounds = new Rect(0, 0, 0, 0);
        GUI.TextField(bounds, "", 0);

        /* GUI style for the font size of the menu */
        GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.fontSize = 20;
        GUIStyle statTextGuiStyle = new GUIStyle();
        statTextGuiStyle.fontSize = 60;

        if (battleMode == true)
        {
            if (gameController.GetPlayerTurnState() == true)
            {
                CharacterStatus playerCharStat = GameObject.Find(curSel.GetSelectedPlayerCharName()).GetComponent<CharacterStatus>();
                CharacterStatus enemyCharStat = GameObject.Find(gameController.GetSelectedEnemy().name).GetComponent<CharacterStatus>();
                /* GUI labels to display the players health and enemy's health */
                GUI.Label(new Rect(Screen.width - 1000 - 20, Screen.height - 550, 250, 50), playerCharStat.GetCurrentHealth().ToString() + " / " + playerCharStat.GetMaxHealth(), statTextGuiStyle);
                GUI.Label(new Rect(Screen.width - 250 - 20, Screen.height - 550, 250, 50), enemyCharStat.GetCurrentHealth().ToString() + " / " + enemyCharStat.GetMaxHealth().ToString(), statTextGuiStyle);

                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 150, 1000, 1000), "Power: " + powerValue, statTextGuiStyle);
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 1000, 1000), "Accuracy: " + accuracyValue, statTextGuiStyle);
                if (activitySequenceState == true)
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.5f, 1000, 1000), "Time: " + buttonMashTimer, statTextGuiStyle);
                }
            }

            if (showHitText == true || showMissText == true)
            {
                if (showHitText == true)
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "HIT", statTextGuiStyle);
                    InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
                }
                else if (showMissText == true)
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "MISS", statTextGuiStyle);
                    InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
                }
            }        
        }
        else if(enemyBattleMode == true)
        {
            if(gameController.GetEnemyTurnState() == true)
            {
                EnemyMove enemyMove = GameObject.Find(enemyAIController.GetEnemyCurrentlyTakingTurn().name).GetComponent<EnemyMove>();
                CharacterStatus playerCharStat = GameObject.Find(enemyMove.GetTarget().name).GetComponent<CharacterStatus>();
                CharacterStatus enemyCharStat = GameObject.Find(enemyAIController.GetEnemyCurrentlyTakingTurn().name).GetComponent<CharacterStatus>();
                GUI.Label(new Rect(Screen.width - 1000 - 20, Screen.height - 550, 250, 50), playerCharStat.GetCurrentHealth().ToString() + " / " + playerCharStat.GetMaxHealth(), statTextGuiStyle);
                GUI.Label(new Rect(Screen.width - 250 - 20, Screen.height - 550, 250, 50), enemyCharStat.GetCurrentHealth().ToString() + " / " + enemyCharStat.GetMaxHealth().ToString(), statTextGuiStyle);
            }

            if (showHitText == true || showMissText == true)
            {
                if (showHitText == true)
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "HIT", statTextGuiStyle);
                    InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
                }
                else if (showMissText == true)
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "MISS", statTextGuiStyle);
                    InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
                }
            }  
        }

        /* Displays the box and button that make up the move confirm menu when the bool is true */
        if (showBattleMenuOptions == true)
        {
            string[] buttonDescriptions = {"Repeatedly press the CONFIRM button to increase power.\n Every " + amountPerPressed + " presses increases power by 1 point",
                                           "Attack is made without additional adjustments",
                                           "Repeatedly press the CONFIRM button to increase accuracy.\n Every " + amountPerPressed + " presses increases accuracy by 1 point"};

            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 70), "Select your Approach", guiStyle);

            GUI.SetNextControlName(battleMenuOptions[0]);
            GUI.Button(new Rect(Screen.width / 2 - 127, Screen.height / 2 - 30, 110, 45), battleMenuOptions[0], guiStyle);

            GUI.SetNextControlName(battleMenuOptions[1]);
            GUI.Button(new Rect(Screen.width / 2 + 6, Screen.height / 2 - 30, 110, 45), battleMenuOptions[1], guiStyle);

            GUI.SetNextControlName(battleMenuOptions[2]);
            GUI.Button(new Rect(Screen.width / 2 + 139, Screen.height / 2 - 30, 110, 45), battleMenuOptions[2], guiStyle);

            GUI.Box(new Rect(Screen.width / 2 - 280, Screen.height / 1.5f, 600, 100), buttonDescriptions[batteMenuOptionsIndex], guiStyle);

            GUI.FocusControl(battleMenuOptions[batteMenuOptionsIndex]);
        }
        else if (displayGOText == true)
        {
            GUIStyle textGuiStyle = new GUIStyle();
            textGuiStyle.fontSize = 80;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 1000, 1000), "GO!", textGuiStyle);
            InvokeRepeating("DisplayTextTimer", 1, 1);
        }
        //else if (powerChosen == true || accuracyChosen == true)
        //{
        //    statTextGuiStyle.fontSize = 60;
        //    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 150, 1000, 1000), "Power: " + powerValue, statTextGuiStyle);
        //    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 1000, 1000), "Accuracy: " + accuracyValue, statTextGuiStyle);
        //    if (activitySequenceState == true)
        //    {
        //        GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.5f, 1000, 1000), "Time: " + buttonMashTimer, statTextGuiStyle);
        //    }

        //    if (showHitText == true || showMissText == true)
        //    {
        //        if (showHitText == true)
        //        {
        //            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "HIT", statTextGuiStyle);
        //            InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
        //        }
        //        else if (showMissText == true)
        //        {
        //            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "MISS", statTextGuiStyle);
        //            InvokeRepeating("DisplayHitMissTextTimer", 1, 1);
        //        }
        //    }
        //}
        //else if (accuracyChosen == true)
        //{
        //    statTextGuiStyle.fontSize = 60;
        //    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 1000, 1000), "Accuracy: " + accuracyValue, statTextGuiStyle);
        //}
        else
        {
            /* When the move confirm menu is not visible then the index is set back to 0 and "No" button being chosen is set to false */
            batteMenuOptionsIndex = 1;
        }
    }

    void MenuSelecting()
    {
        if (showBattleMenuOptions == true)
        {
            // Get keyboard input and increase or decrease our button grid integer
            if (Input.GetButtonDown("Left") && showBattleMenuOptions == true)
            {
                if (batteMenuOptionsIndex == 0)
                {
                    batteMenuOptionsIndex = battleMenuOptions.Length - 1;
                }
                else
                {
                    batteMenuOptionsIndex--;
                }
            }

            if (Input.GetButtonDown("Right") && showBattleMenuOptions == true)
            {
                if (batteMenuOptionsIndex == battleMenuOptions.Length - 1)
                {
                    batteMenuOptionsIndex = 0;
                }
                else
                {
                    batteMenuOptionsIndex++;
                }
            }
        }
    }

    void HandleSelection()
    {
        switch (batteMenuOptionsIndex)
        {
            /* If "Power" is chosen, then... */
            case 0: //Debug.Log(battleMenuOptions[0] + " was chosen ");
                    powerChosen = true;
                    battleStart = true;
                    showBattleMenuOptions = false;
                    displayGOText = true;
                    break;

            /* If "None" is chosen, then... */
            case 1: //Debug.Log(battleMenuOptions[1] + " was chosen ");
                    battleStart = true;
                    showBattleMenuOptions = false;
                    attackInProgress = true;
                    break;

            /* If "Accuracy" is chosen, then... */
            case 2: //Debug.Log(battleMenuOptions[2] + " was chosen ");
                    accuracyChosen = true;
                    battleStart = true;
                    showBattleMenuOptions = false;
                    displayGOText = true;
                    break;
        }
    }

#region Timers
    /* Timers for texts and sequences */
    private void DisplayTextTimer()
    {
        if (--textTimer == 0)
        {
            displayGOText = false;
            //battleInProgress = true;
            activitySequenceState = true;
            CancelInvoke("DisplayTextTimer");
            textTimer = 3;
        }
    }

    private void ButtonMashSequenceTimer()
    {
        if (--buttonMashTimer == 0)
        {
            activitySequenceState = false;
            attackInProgress = true;
            CancelInvoke("ButtonMashSequenceTimer");
            buttonMashTimer = 300;
        }
    }

    private void DisplayHitMissTextTimer()
    {
        if (--hitMissTextTimer == 0 && gameController.GetPlayerTurnState() == true)
        {
            showHitText = false;
            showMissText = false;
            CancelInvoke("DisplayHitMissTextTimer");
            hitMissTextTimer = 2;
        }
        else if (--textTimer == 0 && gameController.GetEnemyTurnState() == true)
        {
            showHitText = false;
            showMissText = false;
            CancelInvoke("DisplayHitMissTextTimer");
            hitMissTextTimer = 2;
        }
    }

    private void TransitionToBattleEnd()
    {
        if(--delayTimer == 0)
        {
            powerChosen = false;
            accuracyChosen = false;
            numTimesButtonIsPressed = 0;        
            transition = true;
            delayTimer = 5;
        }
    }

    private void EnemyTransitionToBattleEnd()
    {
        if(--delayTimer == 0)
        {
            transition = true;
            delayTimer = 5;
        }
    }

#endregion

    /*  */
    private void ButtonMashSequence()
    {        
        //float timer = 0.0f;

        if (Input.GetButtonDown("Confirm"))
        {
            numTimesButtonIsPressed++;
            if (numTimesButtonIsPressed % 5 == 0)
            {
                if (powerChosen == true)
                {
                    powerValue++;
                }
                if(accuracyChosen == true)
                {
                    accuracyValue++;
                }
            }
        }
    }

    /*  */
    private void MakeAttack()
    {
        int rand = Random.Range(0, 100);
        //GameObject.Find(curSel.GetSelectedPlayerCharName()).animation["attack"].wrapMode = WrapMode.Once;
        GameObject.Find(curSel.GetSelectedPlayerCharName()).animation.Play("attack");
        if (rand <= accuracyValue)
        {
            showHitText = true;
            DealDamageToEnemy();
            //Debug.Log("Hits! : " + rand);
        }
        else
        {
            showMissText = true;
            //Debug.Log("No hit :( " + rand);
        }
        attackInProgress = false;
        battleEnd = true;
    }

    private void EnemyAttack()
    {
        int rand = Random.Range(0, 100);
        //GameObject.Find(curSel.GetSelectedPlayerCharName()).animation["attack"].wrapMode = WrapMode.Once;
        GameObject.Find(enemyAIController.GetEnemyCurrentlyTakingTurn().name).animation.Play("punch");
        if (rand <= enemyAccuracyValue)
        {
            showHitText = true;
            DealDamageToPlayer();
        }
        else
        {
            showMissText = true;
        }
        battleEnd = true;
        performEnemyAttack = false;
    }

    private void DealDamageToPlayer()
    {
        EnemyMove enemyMove = GameObject.Find(enemyAIController.GetEnemyCurrentlyTakingTurn().name).GetComponent<EnemyMove>();
        CharacterStatus playerStat = GameObject.Find(enemyMove.GetTarget().name).GetComponent<CharacterStatus>();
        playerStat.TakeDamage(enemyPowerValue);
        
    }

    private void DealDamageToEnemy()
    {
        CharacterStatus enemyCharStat = GameObject.Find(gameController.GetSelectedEnemy().name).GetComponent<CharacterStatus>();
        enemyCharStat.TakeDamage(powerValue);
    }

#region Setters and Getters
    public void SetBattleModeState(bool sbm)
    {
        battleMode = sbm;
    }

    public bool GetBattleModeState()
    {
        return battleMode;
    }

    public void SetEnemyBattleModeState(bool sebms)
    {
        enemyBattleMode = sebms;
    }

    public void SetPerformEnemyAttack(bool spea)
    {
        performEnemyAttack = spea;
    }

    public bool GetPerformEnemyAttack()
    {
        return performEnemyAttack;
    }
#endregion
}
