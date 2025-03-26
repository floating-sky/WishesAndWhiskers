using System;
using UnityEngine;

public class CaretakerPlantScript : MonoBehaviour
{
    public CatScript catScript;
    public LogicScript logicScript;
    public Boolean hasCat;
    private Boolean inTrigger = false;              // Is anything on the plant
    private Collider2D inputObject;                 // the object on the plant

    [SerializeField] private Sprite plantWithCat;
    [SerializeField] private Sprite plantWithoutCat;

    // Update is called once per frame
    void Update()
    {
        // Time Line stop(bath time)
        if (!(Time.timeScale == 0f))
        {
            // Wash the cat with sponge
            if (inTrigger && Input.GetMouseButtonUp(0))
            {
                // Check the input object is sponge
                if (inputObject.gameObject.layer == 7 && hasCat)
                {
                    print("sponge");
                    logicScript.spongeLogic();
                }
            }
        }
    }

    public void CatGoesInside() 
    {
        GetComponent<SpriteRenderer>().sprite = plantWithCat;
        hasCat = true;

    }

    public void CatLeaves() 
    {
        GetComponent<SpriteRenderer>().sprite = plantWithoutCat;
        hasCat = false;
        catScript.CatCleaned();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter");
        inTrigger = true;
        inputObject = collision;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
        inputObject = new Collider2D();
    }
}
