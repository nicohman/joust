/*****************
 * By Tali Edson and Nico Hickman
 * Purpose: Controls the walking enemies in the tutorial
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float timeToAppear = 28.0f;
    private float timer = 0;
    public float timeToDie = 38.0f;
    private float deathTime = 0;
    private float flyTime = 0;
    private bool hitPlayer = false;
    public float timeToFly;
    public float stopFlying;
    private float stopTimer;
    private float flyTimer;
    // Use this for initialization
    void Start()
    {
        timer = timeToAppear;
        deathTime = timeToDie;
        flyTimer = timeToFly;
        stopTimer = stopFlying;
    }
    public void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && timer <= 0 && !hitPlayer)
        {
            hitPlayer = true;
            collision.transform.position = new Vector2(-0.269f, -1.31f);
            collision.gameObject.GetComponent<Animator>().SetInteger("State", 5);
            collision.gameObject.layer = 8;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        deathTime -= Time.deltaTime;
        flyTimer -= Time.deltaTime;
        stopTimer -= Time.deltaTime;
        if(flyTimer <= 0 && stopTimer > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0.6f);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Animator>().SetInteger("State", 2);
        }
    
        if (timer <= 0 && (flyTimer >0  || stopTimer <=0))
        {
            this.gameObject.layer = 10;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Animator>().SetInteger("State", 1);
            this.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.2f, Mathf.Min(0,GetComponent<Rigidbody2D>().velocity.y));
        }
        if (stopTimer <= 0)
        {
            this.gameObject.layer = 10;
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Animator>().SetInteger("State", 1);
            this.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(4f, Mathf.Min(0, GetComponent<Rigidbody2D>().velocity.y));
        }
        if (deathTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
