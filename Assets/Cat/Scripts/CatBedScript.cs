using System;
using UnityEngine;

public class CatBedScript : MonoBehaviour
{
    [SerializeField] public Boolean inTrigger = false;
    public GameObject eButton;
    private Boolean isReady;
    private Boolean isAlreadyCatBed;

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

    public void setIsAlreadyCatBed(Boolean value)
    {
        isAlreadyCatBed = value;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
        if(!isAlreadyCatBed && isReady)
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
