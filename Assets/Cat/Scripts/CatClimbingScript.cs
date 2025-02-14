using System;
using UnityEngine;

public class CatClimbingScript : MonoBehaviour
{
    [SerializeField] public Boolean inTrigger = false;
    public GameObject eButton;
    private Boolean isAlreadyPlayCatClimbing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setIsAlreadyPlayCatClimbing(Boolean value)
    {
        isAlreadyPlayCatClimbing = value;
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
        if(!isAlreadyPlayCatClimbing)
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
