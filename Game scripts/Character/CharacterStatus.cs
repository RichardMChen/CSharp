using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour 
{
    public int movementRange;  // The number of tiles that the character can move
    public int currentHealth;
    public int maxHealth;
    public int attackRange;
    public int power;
    public int accuracy;

    private CursorMovement curMove;
    private CharacterMove charMove;
    private Battle battleController;

	// Use this for initialization
	void Start () 
    {
        currentHealth = maxHealth;
        curMove = GameObject.Find("Cursor").GetComponent<CursorMovement>();
        charMove = gameObject.GetComponent<CharacterMove>();
        battleController = GameObject.Find("GameController").GetComponent<Battle>();
	}

    void OnGUI()
    {
        if (battleController.GetBattleModeState() == false && curMove.GetCurrentRow() == charMove.GetCurRow() && curMove.GetCurrentCol() == charMove.GetCurCol())
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.fontSize = 40;
            //GUIStyle statTextGuiStyle = new GUIStyle();
            //statTextGuiStyle.fontSize = 20;

            //GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 150, 1000, 1000), "Power: " + power, statTextGuiStyle);
            //GUI.Label(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 200, 1000, 1000), "Accuracy: " + accuracy, statTextGuiStyle);
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 1.5f + 90, 400, 100), "Power: " + power  + "\nAccuracy: " + accuracy, guiStyle);
        }
    }

    public int GetMovementRange()
    {
        return movementRange;
    }

    public int GetAttackRange()
    {
        return attackRange;
    }

    public void TakeDamage(int sch)
    {
        if ((currentHealth - sch) <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= sch;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetPowerStat()
    {
        return power;
    }

    public int GetAccuracyStat()
    {
        return accuracy;
    }
}
