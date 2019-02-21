using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    public float jumpForce;
    private Vector2 movement = new Vector2(1, 1);
    public KeyCode jumpKey;
    private Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(this.jumpKey))
        {
            rigid.AddForce(new Vector2(0, this.jumpForce));
        }
	}
}
