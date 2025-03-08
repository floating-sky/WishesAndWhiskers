using System;
using UnityEngine;

public class SofaScript : MonoBehaviour
{
    [SerializeField] public Boolean inTrigger = false;
    [SerializeField] private Sprite damagedSofa;
    [SerializeField] private Sprite newSofa;
    public GameObject eButton;
    private Boolean isAlreadyPlaySofa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIsAlreadyPlaySofa(Boolean value)
    {
        isAlreadyPlaySofa = value;
    }

    public void changeDamageSofa()
    {
        GetComponent<SpriteRenderer>().sprite = damagedSofa;
    }

    public void changeNewSofa()
    {
        GetComponent<SpriteRenderer>().sprite = newSofa;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
        if(!isAlreadyPlaySofa)
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
