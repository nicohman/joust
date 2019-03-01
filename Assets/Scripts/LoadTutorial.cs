/*****************
 * By Nico Hickman
 * Purpose: Loads tutorial scene after a certain amount of time
 *****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadTutorial : MonoBehaviour {
    public float loadTime;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = loadTime;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene("tutorial");
        }
    }
}
