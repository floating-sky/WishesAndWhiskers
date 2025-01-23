using System;
using UnityEngine;

public class BowlsScipt : MonoBehaviour
{
    public LogicScript logicScript;
    private Boolean hasFood = false;
    private Boolean hasWater = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("CareTakerLogic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Boolean getFood()
    {
        return hasFood;
    }

    private Boolean getWater()
    {
        return hasWater;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("This function is running");
        if (Input.GetMouseButtonUp(0))
        {
            print("There is something on me");
            // food
            if (collision.gameObject.layer == 3 && !getFood())
            {
                print("food");
            }

            // water
            if(collision.gameObject.layer == 6 && !getWater())
            {
                print("water");
            }
        }
    }
}
