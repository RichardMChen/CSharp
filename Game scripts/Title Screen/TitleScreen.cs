using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour 
{

	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.anyKeyDown)
        {
            Application.LoadLevel(1);
        }
	}
}
