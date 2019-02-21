using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    private Jump jumper;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public Vector2 speed = new Vector2(10, 10);
	// Use this for initialization
	void Start () {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(this.leftKey))
        {
            this.sprite.flipX = true;
            this.rigid.AddForce(new Vector2(-this.speed.x, 0));
        }
        if (Input.GetKey(this.rightKey))
        {
            this.sprite.flipX = false;
            this.rigid.AddForce(new Vector2(this.speed.x, 0));
        }
	}
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(this.jumpKey.ToString()))) {
            this.jumper.jump();
        }
    }
}
