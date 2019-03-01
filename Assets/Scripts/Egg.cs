/*****************
 * By Nico Hickman
 * Purpose: Control eggs
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
    public EnemyType type = EnemyType.Bounder;
    public int bounty = 0;
    public float waitTime = 2.0f;
    public float animTime = 1.5f;
    private float animTimer;
    private float waitTimer;
    public Enemy enemyBase;
    public GameObject enemyGhost;
    // Use this for initialization
    void Start () {
        this.waitTimer = this.waitTime;
        this.animTimer = this.animTime;
	}
	
	// Update is called once per frame
	void Update () {
        waitTimer -= Time.deltaTime;
        animTimer -= Time.deltaTime;
        if (animTimer < 0)
        {
            GetComponent<Animator>().SetBool("Crack", true);
        }
        if (waitTimer < 0)
        {
            Enemy newEnemy = (Enemy)Instantiate(enemyBase, transform.position, transform.rotation);
            newEnemy.type = type;
            GameObject newGhost = (GameObject)Instantiate(enemyGhost, transform.position, transform.rotation);
            newEnemy.GetComponent<Ghost>().target = newGhost;
            Destroy(this.gameObject);
        }
    }
}
