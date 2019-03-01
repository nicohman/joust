/*****************
 * By Nico Hickman
 * Purpose: Shows text only within a specific interval
 *****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextVisibility : MonoBehaviour
{
    public float startTime;
    public float endTime;
    private float timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < endTime && startTime < timer)
        {
            if (!this.gameObject.GetComponent<Text>().enabled)
            {
                this.gameObject.GetComponent<Text>().enabled = true;
            }
        }
        else
        {
            if (this.gameObject.GetComponent<Text>().enabled)
            {
                this.gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
}
