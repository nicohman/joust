/*****************
 * By Nico Hickman
 * Purpose: Control player
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public GameObject deathParticles;
    private Jump jumper;
    public int lives = 3;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public float speed = 10;
    public int going = 1;
    public float immortalTime = 4.0f;
    private float immortalTimer = 0.0f;
    public AudioSource flapSource;
    private float veloc = 0;
    public float acceleration = 60;
    public bool immortal = false;
    private RespawnPoint[] respawns;
    private RespawnPoint respawnAt;
    private new BoxCollider2D collider;
    public AudioSource deathSource;
    public int lifePer = 5;
    private int nextLife;
    public float respawnTime = 10000f;
    private MountAnimations mountAnim;
    public  float respawnTimer = 0.0f;
    public Canvas canvas;
    public int points = 0;
    public float bounce = 1.2f;
    public Text lifeText;
    public Text scoreText;
    public Text scorePop;
    public Font font;
    public Material fontMaterial;
    public GameOver gameOver;
	// Use this for initialization
	void Start () {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.mountAnim = GetComponent<MountAnimations>();
        this.collider = GetComponent<BoxCollider2D>();
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.nextLife = this.lifePer;
        this.lives--;
        UpdateLifeText();
    }
	
	// Update is called once per frame
	void Update () {
        this.respawnTimer -= Time.deltaTime;
        this.immortalTimer -= Time.deltaTime;
        if (immortalTimer <= 0 && immortal)
        {
            immortal = false;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Animator>().SetInteger("State", 0);
        }
        if (immortal)
        {
            this.gameObject.layer = 8;
        } else
        {
            this.gameObject.layer = 0;
        }
        if (immortal & Input.anyKeyDown)
        {
            immortal = false;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Animator>().SetInteger("State", 0);
        }
        else if (!immortal)
        {
            if (this.respawnTimer <= 0)
            {
                if (rigid.velocity.x > GetComponent<MountAnimations>().walkThreshold)
                {
                    sprite.flipX = false;
                } else if (rigid.velocity.x < -GetComponent<MountAnimations>().walkThreshold){
                    sprite.flipX = true;
                }
                if (!rigid.velocity.y.Equals(0) && going.Equals(1) && sprite.flipX)
                {
                    sprite.flipX = false;
                } else if (!rigid.velocity.y.Equals(0) && going.Equals(-1) && !sprite.flipX)
                {
                    sprite.flipX = true;
                }


                this.rigid.gravityScale = 1.0f;
                if (rigid.velocity.y.Equals(0) && flapSource.isPlaying)
                {
                    flapSource.Stop();
                }
              
                if (Input.GetKey(this.leftKey))
                {
                    going = -1;
                    if (veloc > -(speed * 1.06))
                    {
                        veloc -= speed / acceleration;
                    }
                }
                if (Input.GetKey(this.rightKey))
                {
                    going = 1;
                    if (this.veloc < speed * 1.06)
                    {
                        veloc += speed / acceleration;
                    }
                }
                if (Input.GetKeyDown(this.jumpKey))
                {
                    this.flapSource.Play();
                    this.mountAnim.PlayFlag();
                    this.jumper.jump();
                }
                if (veloc < 0)
                {
                    veloc = Mathf.Max(veloc, -speed);
                }
                else
                {
                    veloc = Mathf.Min(veloc, speed);
                }
                this.rigid.velocity = new Vector2(veloc, rigid.velocity.y);
                UpdateCollider();
            }
        }
    }
    private void UpdateCollider()
    {
        Vector2 spriteSize = sprite.bounds.size;
        collider.size = new Vector2(spriteSize.x/2, spriteSize.y/2);
        collider.offset = new Vector2(0, 0);
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
                int bounty = collision.gameObject.GetComponent<Enemy>().bounty;
                this.points += bounty;
                this.scoreText.text = this.points.ToString("000000");
                ScorePopup(bounty);
                if (this.points > nextLife)
                {
                    nextLife += lifePer;
                    lives++;
                    UpdateLifeText();
                }
            } else if (this.transform.position.y < collision.transform.position.y)
            {
                //Player has lost
                deathSource.Play();
                this.Die();
            }
        }else if (collision.gameObject.tag == "Egg")
        {
            int bounty = collision.gameObject.GetComponent<Egg>().bounty;
            this.points += bounty;
            this.scoreText.text = this.points.ToString("000000");
            ScorePopup(bounty);
            if (this.points > nextLife)
            {
                nextLife += lifePer;
                lives++;
                UpdateLifeText();
            }
            Destroy(collision.gameObject);
        }
        

        }
    public void ScorePopup(int points)
    {
        GameObject popup = new GameObject("popup");
        Text text = popup.AddComponent<Text>();
        text.text = points.ToString("000");
        text.transform.SetParent(canvas.transform);
        text.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y-0.4f, 1);
        text.fontSize = 7;
        text.font = font;
        text.transform.localScale = new Vector3(0.01f, 0.01f);
        text.material = fontMaterial;
        DieAfter die_after = popup.AddComponent<DieAfter>();
        die_after.time = 0.5f;
        popup.transform.SetParent(canvas.transform);
    }
    public void Die()
    {
        if (this.lives > 0)
        {
            this.lives--;
            UpdateLifeText();
            RespawnPoint toRespawn = this.respawns[(int)Mathf.Floor(Random.Range(0.0f, (float)(this.respawns.Length )))];
            Instantiate<GameObject>(deathParticles, transform.position, transform.rotation);
            this.respawnAt = toRespawn;
            this.transform.position = new Vector3(toRespawn.transform.position.x, toRespawn.transform.position.y, toRespawn.transform.position.z);
            this.rigid.velocity = new Vector2(0, 0);
            immortal = true;
            immortalTimer = immortalTime;
            veloc = 0;
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            
            GetComponent<Animator>().SetInteger("State", 5);
            this.respawnTimer = this.respawnTime;
        }
        else
        {
            if (points > ScoreText.highScore)
            {
                ScoreText.highScore = points;
            }
            Instantiate<GameObject>(deathParticles, transform.position, transform.rotation);
            print("game over");
            Enemy[] enemies = Enemy.FindObjectsOfType<Enemy>();
            for(int i = 0; i <enemies.Length; i++) {
                Destroy(enemies[i]);
            }
            gameOver.Lose();
            Destroy(this.gameObject);
            //Player is fully dead
        }
        
    }
}
