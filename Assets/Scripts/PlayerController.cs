using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    private Jump jumper;
    public int lives = 3;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    public Vector2 speed = new Vector2(10, 10);
    public float immortalTime = 1.0f;
    private RespawnPoint[] respawns;
    private RespawnPoint respawnAt;
    private Collider2D collider;
    public float immortalTimer = 0.0f;
    public float respawnTime = 10000f;
    public  float respawnTimer = 0.0f;
    public int points = 0;
    public LifeText lifeText;
	// Use this for initialization
	void Start () {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.collider = GetComponent<Collider2D>();
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.Die();

    }
	
	// Update is called once per frame
	void Update () {
        this.immortalTimer -= Time.deltaTime;
        this.respawnTimer -= Time.deltaTime;
        if (this.respawnTimer <= 0)
        {
            this.rigid.gravityScale = 1.0f;
            if ( this.immortalTimer <= 0f && this.anim.GetBool("Immortal")) {
                this.anim.SetBool("Immortal", false);
            }
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
        if (Mathf.Floor(this.rigid.velocity.x / 10) != 0)
        {
            this.anim.SetBool("Walking", true);
        }
        else
        {
            this.anim.SetBool("Walking", false);
        }
    }
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(this.jumpKey.ToString()))) {
            this.jumper.jump();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (this.rigid.transform.position.y > collision.otherRigidbody.transform.position.y)
            {
                //Player has won
                //collision.gameObject.GetComponent<Enemy>().Die();
                //this.points += collision.gameObject.getComponent<Enemy>().pointValue;
            } else
            {
                //Player has lost
                this.Die();
            }
        }

    }
    public void Die()
    {
        if (this.lives > 0)
        {
            this.lives--;
            RespawnPoint toRespawn = this.respawns[(int)Mathf.Floor(Random.Range(0.0f, (float)(this.respawns.Length )))];
            this.respawnAt = toRespawn;
            this.transform.position = new Vector3(toRespawn.transform.position.x, toRespawn.transform.position.y, toRespawn.transform.position.z);
            this.immortalTimer = this.immortalTime;
            this.anim.SetBool("Immortal", true);
            this.respawnTimer = this.respawnTime;
        }
        else
        {
            print("game over");
            //Player is fully dead
        }
    }
    private void AnimationEvent() {}
}
