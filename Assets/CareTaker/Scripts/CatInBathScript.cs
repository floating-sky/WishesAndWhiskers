using System;
using UnityEngine;

public class CatInBathScript : MonoBehaviour
{
    public BarScript washBarScript;
    private Boolean inTrigger = false;
    private Collider2D inputObject;

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
