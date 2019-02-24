using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {
    public float showTime = 5.0f;
    private float showTimer = 0.0f;
    private Text text;
	// Use this for initialization
	void Start () {
        this.text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (text.enabled)
        {
            showTimer -= Time.deltaTime;
            if (showTimer < 0)
            {
                text.enabled = false;
                SceneManager.LoadScene("menu"); 
            }
        }
    }
    public void Lose()
    {
        text.enabled = true;
        showTimer = showTime;
    }
}
