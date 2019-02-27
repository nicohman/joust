using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour {
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
                        //case TutorialCommand.Actions.DisplayText:
                        this.flapTimer += Time.deltaTime;
                        if (this.flapTimer > 0.3f)
                        {
                            this.flapTimer -= 0.3f;
                            this.mountAnim.PlayFlag();
                            this.jumper.jump();
                        }
                        break;
                    case TutorialCommand.Actions.DisplayText:

                        break;
                    case TutorialCommand.Actions.PlayerHover:
                        {
                            this.flapTimer += Time.deltaTime;
                            if (this.flapTimer > 0.25f)
                            {
                                this.flapTimer -= 0.25f;
                                this.mountAnim.PlayFlag();
                                this.jumper.jump();

                            }
                        }
                        break;
                    case TutorialCommand.Actions.LockPosition:
                        {
                            this.flapTimer += Time.deltaTime;
                            if (this.flapTimer > 0.25f)
                            {
                                this.flapTimer -= 0.25f;
                                this.mountAnim.PlayFlag();
                                this.rigid.constraints = RigidbodyConstraints2D.FreezeAll;

                            }
                        }
                        break;
                    case TutorialCommand.Actions.PlayerFlap:
                        //case TutorialCommand.Actions.DisplayText:
                        this.flapTimer += Time.deltaTime;
                        this.mountAnim.PlayFlag();
                        rigid.AddForce(new Vector2(0, 10));

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
                this.Diee();
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
    public void Diee()
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

    private List<TutorialCommand> commands = new List<TutorialCommand>()
    {
        new TutorialCommand() { StartTime = 0.0f, EndTime = 3.0f, Action =TutorialCommand.Actions.DisplayText, Text="Welcome to Joust"},
        new TutorialCommand() { StartTime = 3.0f, EndTime = 6.0f, Action =TutorialCommand.Actions.DisplayText, Text="To Fly"},
        new TutorialCommand() { StartTime = 6.0f, EndTime = 6.25f, Action =TutorialCommand.Actions.PlayerFlap },
        new TutorialCommand() { StartTime = 7.0f, EndTime = 7.25f, Action =TutorialCommand.Actions.PlayerFlap },
        new TutorialCommand() { StartTime = 8.0f, EndTime = 10.0f, Action =TutorialCommand.Actions.PlayerJump },
        new TutorialCommand() { StartTime = 7.95f, EndTime = 9.0f, Action = TutorialCommand.Actions.PlayerHover },
        new TutorialCommand() { StartTime = 9.0f, EndTime = 18, Action = TutorialCommand.Actions.LockPosition }
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
        LockPosition
    }
    public float StartTime { get; set; }
    public float EndTime { get; set; }
    public Actions Action { get; set; }
    public string Text { get; set; }

}