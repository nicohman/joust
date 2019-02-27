using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
    private RespawnPoint[] respawns;
    public Enemy enemyBase;
    public GameObject enemyGhost;
    public float interval = 4.0f;
    public int max = 3;
    private int count = 0;
    private float timer = 0.0f;
    // Use this for initialization
    void Start () {
        this.respawns = FindObjectsOfType<RespawnPoint>();
        this.timer = interval;
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        if (timer < 0 && count <max)
        {
            count++;
            timer = interval;
            RespawnPoint toRespawn = this.respawns[(int)Mathf.Floor(Random.Range(0.0f, (float)(this.respawns.Length)))];
            Enemy newEnemy = (Enemy)Instantiate(enemyBase, toRespawn.transform.position, toRespawn.transform.rotation);
            newEnemy.type = EnemyType.Bounder;
            Vector3 outsidePos = new Vector3(100.0f, 100.0f);
            GameObject newGhost = (GameObject)Instantiate(enemyGhost,outsidePos, toRespawn.transform.rotation);
            newEnemy.GetComponent<Ghost>().target = newGhost;
        }
    }
}
