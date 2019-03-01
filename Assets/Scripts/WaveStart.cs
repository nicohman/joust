/*****************
 * By Nico Hickman
 * Purpose: Destroy wave text after a specific amount of time
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStart : MonoBehaviour {
    public float showTime = 5.0f;
    private float showTimer = 0;
	// Use this for initialization
	void Start () {
        showTimer = showTime;
	}
	
	// Update is called once per frame
	void Update () {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
