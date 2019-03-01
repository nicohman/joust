/*****************
 * By Tali Edson and Nico Hickman
 * Purpose: Controls flying enemy in the tutorial
 *****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour {
    public float timeToAppear = 11.0f;
    public Egg eggPrefab;
    public float timeToDie = 13.0f;
    private float deathTime = 0;
    private float timer = 0;
    public PhysicsMaterial2D eggMaterial;

    // Use this for initialization
    void Start () {
        timer = timeToAppear;
        deathTime = timeToDie;
        Time.timeScale = 5;
        
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        deathTime -= Time.deltaTime;
        if (timer <= 0)
        {
            GetComponent<Animator>().SetInteger("State",2);
            this.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-0.615f, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (deathTime <= 0)
        {
            Egg egg = Instantiate<Egg>(eggPrefab, transform.position, transform.rotation);
            egg.enabled = false;
            egg.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.1f, egg.GetComponent<Rigidbody2D>().velocity.y);
            egg.GetComponent<Rigidbody2D>().sharedMaterial = eggMaterial;
            egg.gameObject.AddComponent<DieAfter>();
            egg.gameObject.GetComponent<DieAfter>().time = 4;
            egg.gameObject.GetComponent<DieAfter>().timer = 4;

            egg.gameObject.layer = 11;
            Destroy(this.gameObject);
        }
	}
}
