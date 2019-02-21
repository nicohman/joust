using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Vector2 enemyVelocity = new Vector2(5, 0);
    public GameObject player;
    private Rigidbody2D rigid = new Rigidbody2D();
    // Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.transform.position = enemyVelocity;
    }
	
	// Update is called once per frame
	void Update () {
        //if(player Y position > enemy position)
        //  increase vertical position
        //else
        //  decrease vertical position
	}
}
