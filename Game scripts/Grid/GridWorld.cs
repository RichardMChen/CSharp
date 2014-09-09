/* Used to create a 2D array of gameobjects for the grid world and can be edited from 
 * the inspector and gameobjects can be inserted. */
using UnityEngine;
using System.Collections;

[System.Serializable]
/* A class of a row that has a 1D array of gameobjects to represent the columns */
public class Row
{
    public GameObject[] column;
}

public class GridWorld : MonoBehaviour 
{
    public RaycastHit hit;  // The location that the raycast hits
    public Ray ray;  // The raycast
    //public int numRows = 0, numColumns = 0;
    public Row[] row;

	// Use this for initialization
	void Start () 
    {
        //Instantiate(Resources.Load("Robot"), row[4].column[4].transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
    {
        /*if (Input.GetMouseButton(0))  // If the left mouse button is clicked
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // Ray is created from the main camera
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))  // If the raycast hits something
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);  // Draws the raycast in scene
                Debug.Log("Raycast successful");  // Debug message for a successful raycast
                if(hit.collider.gameObject.tag == "GridSquare")
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }*/
        //for (int i = 0; i < numRows; i++)
        //{
        //    for (int j = 0; j < numColumns; j++)
        //    {
        //        Debug.Log(row[i].column[j].name + ": " + row[i].column[j].transform.localPosition);
        //    }
        //}
	}
}
