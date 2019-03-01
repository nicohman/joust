/*****************
 * By Nico Hickman
 * Purpose: Jumps a mount
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    public float jumpForce;
    private Rigidbody2D rigid;
    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

    }
    public void jump()
    {
        rigid.AddForce(new Vector2(0, this.jumpForce));
    }
}
