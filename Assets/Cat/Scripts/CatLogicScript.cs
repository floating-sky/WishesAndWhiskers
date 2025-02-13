using System;
using UnityEngine;

public class CatLogicScript : MonoBehaviour
{
    [SerializeField] private SofaScript sofa;
    [SerializeField] private CatClimbingScript catClimbing;
    [SerializeField] private int currentTasks;
    private int targetTasks;
    private Boolean isAlreadyPlaySofa = false;
    private Boolean isAlreadyPlayCatClimbing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTasks = 2;
        sofa = GameObject.FindGameObjectWithTag("Sofa").GetComponent<SofaScript>();
        catClimbing = GameObject.FindGameObjectWithTag("CatClimbing").GetComponent<CatClimbingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check does the cat has complete all the tasks
        if(currentTasks >= targetTasks)
        {
            print("Cat level end");
            Application.Quit();
        }
    }

    public void CatInteracted()
    {
        // If cat is in the area of sofa
        if(!isAlreadyPlaySofa && sofa.inTrigger)
        {
            print("Cat play with sofa");
            currentTasks++;
            isAlreadyPlaySofa = true;
        }

        // If cat is in the area of cat climbing
        if(!isAlreadyPlayCatClimbing && catClimbing.inTrigger)
        {
            print("Cat play with cat climbing");
            currentTasks++;
            isAlreadyPlayCatClimbing = true;
        }
    }
}
