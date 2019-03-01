/*****************
 * By Nico Hickman
 * Purpose: Keeps a gameobject at a specific offset from another continuously
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    public GameObject target;
    public Vector2 offset;
    public float width = 2.32f;
    private SpriteRenderer targetSprite;
    private SpriteRenderer sprite;
    // Use this for initialization
    void Start () {
        targetSprite = target.GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        targetSprite.sprite = sprite.sprite;
        targetSprite.flipX = sprite.flipX;
        target.transform.position = new Vector3(Mathf.Repeat(transform.position.x+offset.x+width*2, width * 4)-width*2,transform.position.y+offset.y,transform.position.z);
	}
}
