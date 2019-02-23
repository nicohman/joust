using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Bounder,
    Hunter,
    ShadowLord
}
public class Enemy : MonoBehaviour {
    public int bounty = 0;
    public EnemyType type = EnemyType.Bounder;
	// Use this for initialization
	void Start () {
		switch (type)
        {
            case EnemyType.Bounder:
                this.bounty = 500;
                break;
            case EnemyType.Hunter:
                this.bounty = 750;
                break;
            case EnemyType.ShadowLord:
                this.bounty = 1500;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Die()
    {
        //death animation
        //drop egg
        //destroy object
        Destroy(this.gameObject);
    }
}
