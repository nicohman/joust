using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountAnimations : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rigid;
    public Sprite flying;
    private Sprite sprite;
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
        if (Mathf.Floor(this.rigid.velocity.y) != 0)
        {
            this.sprite = this.flying;
            this.anim.SetBool("Walking", false);
            this.anim.SetBool("Flying", true);
        }
        else
        {
            this.anim.SetBool("Flying", false);

            this.sprite = this.idle;

            if (Mathf.Floor(this.rigid.velocity.x) != 0)
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
