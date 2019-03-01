using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountAnimations : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rigid;
    public Sprite flying;
    public bool enemy = true;
    private Sprite sprite;
    public Sprite skidding;
    public AudioSource brakeSource;
    public AudioSource walkSource;
    public Sprite idle;
    public float walkThreshold = 0.05f;
    public float flyThreshold = 0.05f;
    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
        this.rigid = GetComponent<Rigidbody2D>();
    }
	public void PlayFlag()
    {
        this.anim.Play("Base Layer.PlayerFlapping");
    }
	// Update is called once per frame
	void Update () {
        int going = 1;
        int playing = 0;
        if (!enemy)
        {
             going = GetComponent<PlayerController>().going;
        } else
        {
            going = GetComponent<EnemyController>().going;
        }

        if (rigid.velocity.y>0 || (enemy && GetComponent<EnemyController>().ClosestPlayer().transform.position.y + GetComponent<SpriteRenderer>().bounds.extents.y > transform.position.y))
        {
            this.anim.SetInteger("State", 2);
        } else if (rigid.velocity.y < 0)
        {
            this.anim.SetInteger("State", 3);
        } else if (!enemy && ((rigid.velocity.x >= walkThreshold && Input.GetKey(GetComponent<PlayerController>().leftKey) || (rigid.velocity.x <= -walkThreshold && Input.GetKey(GetComponent<PlayerController>().rightKey)))))
        {
            if (!brakeSource.isPlaying )
            {
                playing = 2;
                brakeSource.Play();
            }
            this.anim.SetInteger("State", 4);

        } else if (rigid.velocity.x > walkThreshold || rigid.velocity.x < -walkThreshold)
        {
            if ( !enemy && !walkSource.isPlaying )
            {
                playing = 1;
                walkSource.Play();
            }
            this.anim.SetInteger("State", 1);
        } else if (!this.anim.GetInteger("State").Equals(5))
        {
            this.anim.SetInteger("State", 0);
        }
        if (playing != 1 && !enemy)
        {
            walkSource.Stop();
        }
        if (playing != 2 && !enemy)
        {
            brakeSource.Stop();
        }
    }
}
