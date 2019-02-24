using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour {
    public float width = 2.32f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Repeat(transform.position.x+width, width*2)-width, transform.position.y,transform.position.z);
	}
}
