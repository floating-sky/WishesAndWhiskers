using System;
using UnityEngine;

public class BowlsScript : MonoBehaviour
{
    public LogicScript logicScript;
    public FoodScript foodScript;
    public WaterScript waterScript;
    private Boolean hasFood = false;
    private Boolean hasWater = false;
    private Boolean inTrigger = false;
    private Collider2D inputObject;
    [SerializeField] public GameObject cat;

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
        // Change the visual cue for food and water, and add food and water to the bowls
        if(inTrigger)
        {
            // food
            if (inputObject.gameObject.layer == 3)
            {
                foodScript.changeDropingFood();                                 // visual cue change for food
                if(Input.GetMouseButtonUp(0) && !GetFood())                     // if mouse released and bowl doesn't have food, add food 
                {
                    hasFood = true;
                }
            }
            // water
            if (inputObject.gameObject.layer == 4)
            {
                waterScript.changeDropingWater();                               // visual cue change for water
                if(Input.GetMouseButtonUp(0) && !GetWater())                    // if mouse released and bowl doesn't have water, add water
                {
                    hasWater = true;
                }
            }
        }else{
            foodScript.changeFood();                                            // change the food back to normal
            waterScript.changeWater();                                          // change the water back to normal
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

        if (hasFood)
            cat.GetComponent<CatScript>().EatFood();
        else if (hasWater)
            cat.GetComponent<CatScript>().DrinkWater();
    }

    private Boolean GetFood()
    {
        return hasFood;
    }

    private Boolean GetWater()
    {
        return hasWater;
    }

    public void SetFood(Boolean hasFood)
    {
        this.hasFood = hasFood;

        // Cat has ate food
        if(!hasFood)
        {
            logicScript.CatAteFoodORWater();
        }
    }

    public void SetWater(Boolean hasWater)
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
