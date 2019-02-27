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
    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<Sprite>();
    }
	public void PlayFlag()
    {
        this.anim.Play("Base Layer.PlayerFlapping");
    }
	// Update is called once per frame
	void Update () {
        int going = 1;
        if (!enemy)
        {
             going = GetComponent<PlayerController>().going;
        } else
        {
            going = GetComponent<EnemyController>().going;
        }
        if (rigid.velocity.y>0)
        {
            this.anim.SetInteger("State", 2);
        } else if (rigid.velocity.y < 0)
        {
            this.anim.SetInteger("State", 3);
        } else if ((rigid.velocity.x > walkThreshold && going < 0) || (rigid.velocity.x < -walkThreshold && going > 0))
        {
            if (!brakeSource.isPlaying && !enemy)
            {
                brakeSource.Play();
            }
            this.anim.SetInteger("State", 4);
        } else if (rigid.velocity.x > walkThreshold || rigid.velocity.x < -walkThreshold)
        {
            if (!walkSource.isPlaying && !enemy)
            {
                walkSource.Play();
            }
            this.anim.SetInteger("State", 1);
        } else
        {
            this.anim.SetInteger("State", 0);
        }
  
    }
}
