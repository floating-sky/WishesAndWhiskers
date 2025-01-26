using System;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public Boolean isHungry = false;
    public Boolean isThirsty = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean GetHungry()
    {
        return isHungry;
    }

    public void SetHungry(Boolean isHungry)
    {
        this.isHungry = isHungry;
        print("Cat is Hungry");
    }

    public Boolean GetThirsty()
    {
        return isThirsty;
    }

    public void SetThirsty(Boolean isThirsty)
    {
        this.isThirsty = isThirsty;
        print("Cat is Thirsty");
    }
}
