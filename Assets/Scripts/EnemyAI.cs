using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Vector3 enemyVelocity = new Vector3(0, 0,0);
    public int flapStrength = 10;
    public int speed = 1;
    public PlayerController player;
    private Rigidbody2D rigid = new Rigidbody2D();
    private Jump jumper;
    private int going = 1;
    public float jumpTimer = 0;
    public float jumpInterval = 1.0f;
    // Use this for initialization
	void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumper = GetComponent<Jump>();
        if (Random.Range(0,2) > 1) {
            this.going = -1;
        }
    }
	public PlayerController ClosestPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in players)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        this.player = ClosestPlayer();
        jumpTimer -= Time.deltaTime;
        if(player.transform.position.y > transform.position.y)
        {
            //if player is higher than enemy, flap wings
            if (jumpTimer < 0)
            {
                jumper.jump();
                jumpTimer = jumpInterval;
            }
        }

       rigid.AddForce(new Vector2(going * speed, 0));
        rigid.velocity = new Vector2(Mathf.Min(rigid.velocity.x, speed), 0);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        going = -1 * going;
    }

}
