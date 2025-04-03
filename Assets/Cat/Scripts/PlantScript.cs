using System;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] public Boolean inTrigger = false;
    [SerializeField] private Sprite catInPlant;
    public GameObject eButton;
    private Boolean isReady;
    private Boolean isAlreadyPlant;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean getIsReady()
    {
        return isReady;
    }

    public void setIsReady(Boolean value)
    {
        isReady = value;
    }

    public void setIsAlreadyPlant(Boolean value)
    {
        isAlreadyPlant = value;
    }

    public void changeView(int ind){
        switch(ind)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = catInPlant;
                break;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
        if(!isAlreadyPlant && isReady)
        {
            eButton.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
        eButton.SetActive(false);
    }
}
