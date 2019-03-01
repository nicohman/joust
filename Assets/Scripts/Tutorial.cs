/*****************
 * By Tali Edson and Nico Hickman
 * Purpose: Controls tutorial animation
*****************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private Jump jumper;
    public int lives = 0;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    public Vector2 speed = new Vector2(10, 10);
    public float immortalTime = 1.0f;
    private RespawnPoint[] respawns;
    private RespawnPoint respawnAt;
    private new Collider2D collider;
    public float immortalTimer = 0.0f;
    public int lifePer = 5;
    private int nextLife;
    public float respawnTime = 10000f;
    private MountAnimations mountAnim;
    public float respawnTimer = 0.0f;
    public int points = 0;
    public Text lifeText;
    public Text scoreText;
    public GameOver gameOver;
    public bool tutorial = false;
    public float flapTimer = 0.0f;
    public float tutorialTimer = 0.0f;
    private int hover = 1;
    void Start()
    {
        this.jumper = GetComponent<Jump>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.mountAnim = GetComponent<MountAnimations>();
        this.collider = GetComponent<Collider2D>();
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.nextLife = this.lifePer;
        this.lives--;
        Physics2D.gravity = new Vector2(0, -9.81f);
        UpdateLifeText();
    }
    void Update()
    {
        if (tutorial)
        {
            updateTutorial();
        }
        else
        {
            updateGamemode();
        }
    }

    private void updateTutorial()
    {
        this.tutorialTimer += Time.deltaTime;

        // Look at all the commands and do the things that are within the timeframe
        foreach (var command in commands)
        {
            // In timeframe?
            if ((tutorialTimer >= command.StartTime) && (tutorialTimer <= command.EndTime))
            {
                switch (command.Action)
                {
                    case TutorialCommand.Actions.PlayerJump:
                        //GetComponent<Jump>().jumpForce = 13.0f;
                        this.flapTimer += Time.deltaTime;
                        if (this.flapTimer > 0.2f)
                        {
                            this.flapTimer -= 0.2f;
                            this.mountAnim.PlayFlag();
                            rigid.velocity = new Vector2(rigid.velocity.x, 1.7f);
                        }
                        break;

                    case TutorialCommand.Actions.PlayerHover:
                        {

                            this.mountAnim.PlayFlag();
                            rigid.velocity = new Vector2(rigid.velocity.x, -0.1f);
                            rigid.gravityScale = 0;

                        }
                        break;
                    case TutorialCommand.Actions.LockPosition:
                        {
                            this.flapTimer += Time.deltaTime;
                            if (this.flapTimer > 0.25f)
                            {
                                this.flapTimer -= 0.25f;
                                this.mountAnim.PlayFlag();
                                rigid.gravityScale = 0;
                                
                          
                                    if (hover % 2 == 0)
                                    {
                                        rigid.velocity = new Vector2(rigid.velocity.x, -0.05f);
                                    }
                                    else
                                    {

                                        rigid.velocity = new Vector2(rigid.velocity.x, 0.05f);
                                    }
                                    hover++;
                                

                            }
                        }
                        break;
                    case TutorialCommand.Actions.PlayerFlap:
                        {
                            this.flapTimer += Time.deltaTime;
                            if (this.flapTimer > 0.2f)
                            {
                                this.flapTimer += Time.deltaTime;
                                this.mountAnim.PlayFlag();
                                rigid.velocity = new Vector2(rigid.velocity.x, 1.1f);
                            }
                        }
                        break;
                    case TutorialCommand.Actions.Drop:
                        {
                            this.flapTimer += Time.deltaTime;
                            if (this.flapTimer > 0.2f)
                            {
                                this.flapTimer += Time.deltaTime;
                                
                                rigid.gravityScale = 1;
                            }
                        }
                        break;

                    case TutorialCommand.Actions.PlayerRun:
                        {
                            this.GetComponent<SpriteRenderer>().enabled = true;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, GetComponent<Rigidbody2D>().velocity.y);
                            GetComponent<Animator>().SetInteger("State", 1);
                        }
                    
                        break;
                    case TutorialCommand.Actions.PlayerMove:
                        {
                           // this.GetComponent<SpriteRenderer>().enabled = true;
                            //GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, GetComponent<Rigidbody2D>().velocity.y);
                            //transform.position = new Vector2(-0.41f, -1.31f);
                            GetComponent<Animator>().SetInteger("State", 5);
                            //gameObject.layer = 8;
                        }
                        break;
                    case TutorialCommand.Actions.End:
                        SceneManager.LoadScene("menu");
                        break;
                    case TutorialCommand.Actions.PlayerStop:
                        GetComponent<Animator>().SetInteger("State", 0);
                        break;

                }
            }
        }
    }
    private void updateGamemode()
    {
        this.immortalTimer -= Time.deltaTime;
        this.respawnTimer -= Time.deltaTime;
    }
   
    private void UpdateLifeText()
    {
        this.lifeText.text = this.lives.ToString();


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Egg")
        {
            Destroy(collision.gameObject);
        }


    }

    private void AnimationEvent() { }

    private List<TutorialCommand> commands = new List<TutorialCommand>()
    {
        new TutorialCommand() { StartTime = 0.0f, EndTime = 3.0f, Action =TutorialCommand.Actions.DisplayText, Text="Welcome to Joust"},
        new TutorialCommand() { StartTime = 3.0f, EndTime = 6.0f, Action =TutorialCommand.Actions.DisplayText, Text="To Fly"},
        new TutorialCommand() { StartTime = 6.0f, EndTime = 6.25f, Action =TutorialCommand.Actions.PlayerFlap },
        new TutorialCommand() { StartTime = 7.0f, EndTime = 7.25f, Action =TutorialCommand.Actions.PlayerFlap },
        new TutorialCommand() { StartTime = 8.0f, EndTime = 10.0f, Action =TutorialCommand.Actions.PlayerJump },
        new TutorialCommand() { StartTime = 10.0f, EndTime = 13.0f, Action = TutorialCommand.Actions.PlayerHover },
        new TutorialCommand() { StartTime = 9.0f, EndTime = 16.0f, Action = TutorialCommand.Actions.LockPosition },
        new TutorialCommand() { StartTime = 16.0f, EndTime = 16.25f, Action = TutorialCommand.Actions.PlayerFlap },
        new TutorialCommand() { StartTime = 16.25f, EndTime = 17.0f, Action = TutorialCommand.Actions.Drop },
        new TutorialCommand() { StartTime = 18.00f, EndTime = 21.0f, Action = TutorialCommand.Actions.PlayerRun },
        new TutorialCommand() { StartTime = 21.0f, EndTime = 21.1f, Action = TutorialCommand.Actions.PlayerStop },
        new TutorialCommand() { StartTime = 42.00f, EndTime = 43.0f, Action = TutorialCommand.Actions.PlayerMove },
        new TutorialCommand() { StartTime = 47.00f, EndTime = 49.0f, Action = TutorialCommand.Actions.PlayerJump },
        new TutorialCommand() { StartTime = 49.0f, EndTime = 55.0f, Action = TutorialCommand.Actions.LockPosition },
        new TutorialCommand() { StartTime = 57.00f, EndTime = 58.00f, Action = TutorialCommand.Actions.End },
    };
}


public class TutorialCommand
{
    public enum Actions
    {
        DisplayText,
        PlayerJump,
        PlayerHover,
        PlayerFlap,
        Drop,
        LockPosition,
        PlayerRun,
        PlayerMove,
        End,
        PlayerStop
    }
    public float StartTime { get; set; }
    public float EndTime { get; set; }
    public Actions Action { get; set; }
    public string Text { get; set; }

}