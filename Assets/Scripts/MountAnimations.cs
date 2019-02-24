using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountAnimations : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rigid;
    public Sprite flying;
    private Sprite sprite;
    public Sprite skidding;
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
        if (rigid.velocity.y > flyThreshold || rigid.velocity.y < -flyThreshold)
        {
            this.sprite = this.flying;
            this.anim.SetBool("Walking", false);
            this.anim.SetBool("Flying", true);
        }
        else
        {
            this.anim.SetBool("Flying", false);

            this.sprite = this.idle;
            int going = GetComponent<PlayerController>().going;
            if ((rigid.velocity.x > walkThreshold  && going < 0) || (rigid.velocity.x < -walkThreshold && going > 0))
            {
                print("skid");
                this.anim.SetBool("Walking", false);
                this.GetComponent<SpriteRenderer>().sprite = this.skidding;
            } else 
            if (rigid.velocity.x > walkThreshold || rigid.velocity.x < -walkThreshold)
            {
                this.anim.SetBool("Walking", true);
            }
            else
            {
                this.anim.SetBool("Walking", false);
            }
        }
    }
}
