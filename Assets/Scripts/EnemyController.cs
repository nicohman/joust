/*****************
 * By Nico Hickman
 * Purpose: Controls enemies automatically
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float speed = 10;
    public float maxSpeed = 20;
    public int going = 1;
    private Jump jumper;
    public float jumpInterval = 1.0f;
    private float jumpTimer = 0;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private BoxCollider2D col;
    private Animator anim;
	// Use this for initialization
	void Start () {
        this.jumpTimer = jumpInterval;
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.col = GetComponent<BoxCollider2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject target = ClosestPlayer().gameObject;
        jumpTimer -= Time.deltaTime;
		if (target.transform.position.y + (0.25) >= transform.position.y)
        {
            this.anim.SetInteger("State",1);
            jumpTimer = jumpInterval;
            jumper.jump();
        } else if (!rigid.velocity.y.Equals(0))
        {
            this.anim.SetInteger("State", 2);
        } else
        {
            this.anim.SetInteger("State", 0);

        }
       
        rigid.AddForce(new Vector2(going * speed, 0));
        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
        if (this.going.Equals(1) && this.sprite.flipX)
        {
            this.sprite.flipX = false;
        } else if (!this.going.Equals(1) && !this.sprite.flipX)
        {
            this.sprite.flipX = true;
        }
	}
    public PlayerController ClosestPlayer()
    {
        return PlayerController.FindObjectOfType<PlayerController>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.position.y <= transform.position.y + col.bounds.extents.y && collision.transform.position.y +collision.collider.bounds.extents.y >=  transform.position.y) || collision.gameObject.tag == "Enemy")
        {
            going = going * -1;

        }
    }
}
