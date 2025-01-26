using System;
using UnityEngine;

public class BowlsScipt : MonoBehaviour
{
    public LogicScript logicScript;
    private Boolean hasFood = false;
    private Boolean hasWater = false;
    private Boolean inTrigger = false;
    private Collider2D inputObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("CareTakerLogic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger && Input.GetMouseButtonUp(0))
        {
            // food
            if (inputObject.gameObject.layer == 3 && !getFood())
            {
                print("food");
            }

            // water
            if(inputObject.gameObject.layer == 4 && !getWater())
            {
                print("water");
            }
        }
    }

    private Boolean getFood()
    {
        return hasFood;
    }

    private Boolean getWater()
    {
        return hasWater;
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
