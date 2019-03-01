/*****************
 * By Tali Edson
 * Purpose: Controls the tutorial pterodactyl
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pterodactyl : MonoBehaviour
{
    public float timeToAppear = 11.0f;

    public float timeToDie = 13.0f;
    private float deathTime = 0;
    private float timer = 0;
    public GameObject deathParticles;

    // Use this for initialization
    void Start()
    {
        timer = timeToAppear;
        deathTime = timeToDie;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        deathTime -= Time.deltaTime;
        if (timer <= 0)
        {
            //GetComponent<Animator>().SetInteger("State", 2);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1.1f, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (deathTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate<GameObject>(deathParticles, collision.transform.position, collision.transform.rotation);

            Destroy(collision.gameObject);
        }
    }
}
