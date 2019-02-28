using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    private Jump jumper;
    public int lives = 3;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private float veloc = 0;
    public float acceleration = 60;
    public float immortalTime = 1.0f;
    private RespawnPoint[] respawns;
    private RespawnPoint respawnAt;
    private new BoxCollider2D collider;
    public AudioSource deathSource;
    public AudioSource brakeSource;
    public AudioSource walkSource;
    private bool brakeOneshot;
    public float immortalTimer = 0.0f;
    public int lifePer = 5;
    private int nextLife;
    public float respawnTime = 10000f;
    public float respawnTimer = 0.0f;
    public int points = 0;
    public float bounce = 1.2f;
    public Text lifeText;
    public Text scoreText;
    public GameOver gameOver;

    public Vector2 speed = new Vector2(50, 110);
    public float maxSpeed = 1;
    public float walkThreshold = 0.05f;
    public float flyThreshold = 0.05f;

    // Use this for initialization
    void Start()
    {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.anim = GetComponent<Animator>();
        this.nextLife = this.lifePer;
        this.lives--;
        UpdateLifeText();
    }

    // Update is called once per frame
    void Update()
    {
        this.immortalTimer -= Time.deltaTime;
        if (this.respawnTimer > 0)
        {
            this.respawnTimer -= Time.deltaTime;
            return;
        }

        //if ( this.immortalTimer <= 0f && this.anim.GetBool("Immortal")) {
        //    this.anim.SetBool("Immortal", false);
        //}

        bool flying = Mathf.Abs(this.rigid.velocity.y) > flyThreshold;
        this.rigid.drag = flying ? 1.5f : 0f;

        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(this.leftKey))
        {
            moveDirection += new Vector2(-1, 0);
        }
        if (Input.GetKey(this.rightKey))
        {
            moveDirection += new Vector2(1, 0);
        }
        if (Input.GetKeyDown(this.jumpKey))
        {
            moveDirection += new Vector2(0, 1);
        }

        if (moveDirection != Vector2.zero)
        {
            print(moveDirection);
            this.sprite.flipX = (moveDirection.x < 0);

            if (Mathf.Sign(moveDirection.x) != Mathf.Sign(rigid.velocity.x))
            {
                if (!flying)
                {
                    if (brakeOneshot)
                    {
                        brakeSource.Play();
                        brakeOneshot = false;
                    }
                    walkSource.Stop();
                }
                else
                {
                    brakeSource.Stop();
                    brakeOneshot = true;
                }
            }
            else if (Mathf.Abs(this.rigid.velocity.x) >= maxSpeed)
            {
                moveDirection.x = 0;
            }
        }

        this.rigid.AddForce(Vector2.Scale(moveDirection, speed));

        if (!flying && !brakeSource.isPlaying && Mathf.Abs(this.rigid.velocity.x) > walkThreshold && !walkSource.isPlaying)
        {
            walkSource.Play();
        }

        /*if (Mathf.Abs(rigid.velocity.y) > flyThreshold)
        {
            this.anim.SetBool("Flying", true);
        }
        else
        {
            this.anim.SetBool("Flying", false);
            this.anim.SetBool("Walking", Mathf.Abs(this.rigid.velocity.x) > walkThreshold);
        }*/

        UpdateCollider();
    }
    private void UpdateCollider()
    {
        Vector2 spriteSize = sprite.bounds.size;
        collider.size = new Vector2(spriteSize.x / 2, spriteSize.y / 2);
        collider.offset = Vector2.zero;
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

            }
            else if (this.transform.position.y < collision.transform.position.y)
            {
                //Player has lost
                deathSource.Play();
                this.Die();
            }
        }
        else if (collision.gameObject.tag == "Egg")
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
            RespawnPoint toRespawn = this.respawns[(int)Mathf.Floor(Random.Range(0.0f, (float)(this.respawns.Length)))];
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
    private void AnimationEvent() { }
}
