using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
    public EnemyType type = EnemyType.Bounder;
    public int bounty = 0;
    public float waitTime = 2.0f;
    private float waitTimer;
    public Enemy enemyBase;
	// Use this for initialization
	void Start () {
        this.waitTimer = this.waitTime;
	}
	
	// Update is called once per frame
	void Update () {
        waitTimer -= Time.deltaTime;
        if (waitTimer < 0)
        {
            Enemy newEnemy = (Enemy)Instantiate(enemyBase, transform.position, transform.rotation);
            newEnemy.type = type;
            Destroy(this.gameObject);
        }
    }
}
