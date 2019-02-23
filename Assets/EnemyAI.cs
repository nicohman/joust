using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Vector3 enemyVelocity = new Vector3(5, 0);
    public int flapStrength = 10;
    public PlayerController player;
    private Rigidbody2D rigid = new Rigidbody2D();

    // Use this for initialization
	void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.transform.position = enemyVelocity;
    }
	
	// Update is called once per frame
	void Update()
    {
        
        if(player.transform.position.y > rigid.transform.position.y)
        {
            //if player is higher than enemy, flap wings
            Flap();
        }

        //gravity
        enemyVelocity.y--;

        //update enemy position
        rigid.transform.Translate(enemyVelocity);
	}
    private void Flap()
    {
        //enemy flap animation
        //increase vertical in velocity vector
        enemyVelocity.y += flapStrength;
    }
    private void OnDestroy()
    {
        //death animation
        //drop egg
        //destroy object
        Destroy(this.gameObject);
    }
}
