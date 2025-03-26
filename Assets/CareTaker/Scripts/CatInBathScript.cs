using System;
using UnityEngine;
using UnityEngine.UI;

public class CatInBathScript : MonoBehaviour
{
    public BarScript washBarScript;
    public Slider bar;
    private Boolean inTrigger = false;
    private Collider2D inputObject;

    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;
    public Sprite frame5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //washBarScript = GameObject.FindGameObjectWithTag("WashBar").GetComponent<BarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger)
        {
            washBarScript.IncreaseProgressBar(0.001f);
        }

        if (bar.value < .2)
            GetComponent<SpriteRenderer>().sprite = frame1;
        else if (bar.value < .4)
            GetComponent<SpriteRenderer>().sprite = frame2;
        else if (bar.value < .6)
            GetComponent<SpriteRenderer>().sprite = frame3;
        else if (bar.value < .8)
            GetComponent<SpriteRenderer>().sprite = frame4;
        else if (bar.value < 1)
            GetComponent<SpriteRenderer>().sprite = frame5;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
        inputObject = collision;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
        inputObject = new Collider2D();
    }
}
