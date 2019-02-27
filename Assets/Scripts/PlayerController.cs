
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    private Jump jumper;
    public int lives = 3;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public Vector2 speed = new Vector2(10, 10);
    public float immortalTime = 1.0f;
    private RespawnPoint[] respawns;
    private RespawnPoint respawnAt;
    private new BoxCollider2D collider;
    public AudioSource deathSource;
    public float immortalTimer = 0.0f;
    public int lifePer = 5;
    private int nextLife;
    public float respawnTime = 10000f;
    private MountAnimations mountAnim;
    public  float respawnTimer = 0.0f;
    public int points = 0;
    public Text lifeText;
    public Text scoreText;
    public GameOver gameOver;

    public float flapTimer = 0.0f;
    public float tutorialTimer = 0.0f;

    // Use this for initialization
    void Start () {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.mountAnim = GetComponent<MountAnimations>();
        this.collider = GetComponent<Collider2D>();
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.nextLife = this.lifePer;
        this.lives--;
        UpdateLifeText();
    }

    // Update is called once per frame
    void Update()
    {
        this.immortalTimer -= Time.deltaTime;
        this.respawnTimer -= Time.deltaTime;

        if (this.respawnTimer <= 0)
        {
            this.rigid.gravityScale = 1.0f;
            /*if ( this.immortalTimer <= 0f && this.anim.GetBool("Immortal")) {
                this.anim.SetBool("Immortal", false);
            }*/
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
            if (Input.GetKeyDown(this.jumpKey))
            {
                this.mountAnim.PlayFlag();
                this.jumper.jump();
            }

        }
    }
   
    
    private void updateGamemode()
    {
        this.immortalTimer -= Time.deltaTime;
        this.respawnTimer -= Time.deltaTime;

        if (this.respawnTimer <= 0)
        {
            this.rigid.gravityScale = 1.0f;
            /*if ( this.immortalTimer <= 0f && this.anim.GetBool("Immortal")) {
                this.anim.SetBool("Immortal", false);
            }*/
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
            if (Input.GetKeyDown(this.jumpKey))
            {
                this.mountAnim.PlayFlag();
                this.jumper.jump();
            }

        }
    }
    private void OnGUI()
    {

    }
    private void UpdateLifeText()
    {
        this.lifeText.text = this.lives.ToString();


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (this.transform.position.y > collision.transform.position.y)
            {
                //Player has won
                collision.gameObject.GetComponent<Enemy>().Die();

            } else if (this.transform.position.y < collision.transform.position.y)
            {
                //Player has lost
                deathSource.Play();
                this.Die();
            }
        }else if (collision.gameObject.tag == "Egg")
        {
            this.points += collision.gameObject.GetComponent<Egg>().bounty;
            this.scoreText.text = this.points.ToString("000000");
            if (this.points > nextLife)
            {
                nextLife += lifePer;
                lives++;
                UpdateLifeText();
            }
            Destroy(collision.gameObject);
        }

    }
    public void Die()
    {
        if (this.lives > 0)
        {
            this.lives--;
            UpdateLifeText();
            RespawnPoint toRespawn = this.respawns[(int)Mathf.Floor(Random.Range(0.0f, (float)(this.respawns.Length )))];
            this.respawnAt = toRespawn;
            this.transform.position = new Vector3(toRespawn.transform.position.x, toRespawn.transform.position.y, toRespawn.transform.position.z);
            this.immortalTimer = this.immortalTime;
            this.respawnTimer = this.respawnTime;
        }
        else
        {
            print("game over");
            gameOver.Lose();
            Destroy(this.gameObject);
            //Player is fully dead
        }
    }
  
 
}


