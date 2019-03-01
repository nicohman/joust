/*****************
 * By Nico Hickman
 * Purpose: Destroys a gameobject after a specific time
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter : MonoBehaviour {
    public float time = 0.25f;
    public float timer = 0;
	// Use this for initialization
	void Start () {
        timer = time;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
		if (timer <=0)
        {
            Destroy(this.gameObject);
        }
	}
}
