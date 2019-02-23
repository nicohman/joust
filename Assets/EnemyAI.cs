using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Vector3 enemyVelocity = new Vector3(0, 0,0);
    public int flapStrength = 10;
    public int speed = 2;
    public PlayerController player;
    private Rigidbody2D rigid = new Rigidbody2D();
    private Jump jumper;
    private float jumpTimer = 0;
    public float jumpInterval = 1.0f;
    // Use this for initialization
	void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumper = GetComponent<Jump>();
    }
	
	// Update is called once per frame
	void Update()
    {
        jumpTimer -= Time.deltaTime;
        if(player.transform.position.y > rigid.transform.position.y)
        {
            //if player is higher than enemy, flap wings
            if (jumpTimer < 0)
            {
                jumper.jump();
                jumpTimer = jumpInterval;
            }
        }
        if (player.transform.position.x < rigid.transform.position.x)
        {
            enemyVelocity.x -= speed;
        } else
        {
            enemyVelocity.x += speed;
        }

        //update enemy position
        rigid.AddForce(new Vector2(enemyVelocity.x,enemyVelocity.y));
        this.enemyVelocity = new Vector3(0, 0,0);
	}
  
  
}
