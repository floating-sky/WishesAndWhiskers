using System;
using UnityEngine;

public class BowlsScipt : MonoBehaviour
{
    public LogicScript logicScript;
    private Boolean hasFood = false;
    private Boolean hasWater = false;
    private Boolean inTrigger = false;
    private Collider2D inputObject;

    [SerializeField] private Sprite noFoodNoWater;                          // Bowls doesn't have food and water
    [SerializeField] private Sprite foodNoWater;                            // Bowls has food but doesn't have water
    [SerializeField] private Sprite noFoodWater;                            // Bowls doesn't have food but have water
    [SerializeField] private Sprite foodWater;                              // Bowls has food and water

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasFood = false;
        hasWater = false;
        logicScript = GameObject.FindGameObjectWithTag("CareTakerLogic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetMouseButtonUp(0))
        {
            // food
            if (inputObject.gameObject.layer == 3 && !GetFood())
            {
                hasFood = true;
            }

            // water
            if (inputObject.gameObject.layer == 4 && !GetWater())
            {
                hasWater = true;
            }
        }

        // Check the bowls has food or water, the image change
        if (hasFood)
        {
            if (hasWater)
            {
                GetComponent<SpriteRenderer>().sprite = foodWater;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = foodNoWater;
            }
        }
        else
        {
            if (hasWater)
            {
                GetComponent<SpriteRenderer>().sprite = noFoodWater;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = noFoodNoWater;
            }
        }
    }

    private Boolean GetFood()
    {
        return hasFood;
    }

    private Boolean GetWater()
    {
        return hasWater;
    }

    private void SetFood(Boolean hasFood)
    {
        this.hasFood = hasFood;

        // Cat has ate food
        if(!hasFood)
        {
            logicScript.CatAteFoodORWater();
        }
    }

    private void SetWater(Boolean hasWater)
    {
        this.hasWater = hasWater;

        // Cat has drink water
        if(!hasWater)
        {
            logicScript.CatAteFoodORWater();
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
