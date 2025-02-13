using System;
using UnityEngine;

public class CatClimbingScript : MonoBehaviour
{
    [SerializeField] public Boolean inTrigger = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }
}
