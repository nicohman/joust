/*****************
 * By Nico Hickman
 * Purpose: Controls the main menu selection
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectMenu : MonoBehaviour {
    public int select = 0;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode pickKey;
    public Text[] picks;
	// Use this for initialization
	void Start () {
        picks = this.gameObject.GetComponentsInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(pickKey))
        {
            SceneManager.LoadScene("arena");
        } else 
        if (Input.GetKeyDown(upKey))
        {
            if (select != 0)
            {
                select--;
            } else
            {
                select = picks.Length - 1;
            }
        } else if (Input.GetKeyDown(downKey))
        {
            if (select != (picks.Length -1))
            {
                select++;

            }
            else
            {
                select = 0;
            }
        }
        for (int i =0; i < picks.Length; i++)
        {
            picks[i].text = "";
        }
        picks[select].text = "*";
	}
}
