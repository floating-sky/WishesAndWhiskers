using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatScript : MonoBehaviour
{

    [SerializeField]
    public AudioSource hungryMeow;

    [SerializeField]
    public AudioSource thirstyMeow;

    public Boolean isHungry = false;
    public Boolean isThirsty = false;

    // Cat is busy when they are doing an action that should not be interuptted
    public Boolean isBusy = false;

    // Cat is expressing a need through behavior. Used so that random movement doesn't interupt cat during behavior animation, but other actions CAN interupt
    public Boolean isDoingBehavior = false;

    public int moveRadius = 6; // How far from their current position the cat will move when they move to a random point
    public int hungryMeowInterval = 15; // Seconds between each meow the cat makes when they are hungry
    public int thirstyMeowInterval = 15;
    public int randomMovementInterval = 6; // Seconds between each time the cat moves to a random location

    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        InvokeRepeating("MoveCatToRandomDestination", 1, randomMovementInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Grabs a random point from the walkable area (within the given radius)
    public Vector3 GetRandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Called at each randomMovementInterval
    public void MoveCatToRandomDestination() 
    {
        if (!isBusy && !isDoingBehavior)
        {
            agent.SetDestination(GetRandomNavmeshLocation(moveRadius));
        }
    }

    public void CatHungryBehavior() 
    {
        if (!isBusy) 
        {
            isDoingBehavior = true;
            agent.isStopped = true;
            int secondsToWait = 3;
            hungryMeow.Play();
            StartCoroutine(WaitForAnimation(secondsToWait));
            print("Cat is meowing (HUNGRY)");
        }
    }

    public void CatThirstyBehavior() 
    {
        if (!isBusy)
        {
            isDoingBehavior = true;
            agent.isStopped = true;
            int secondsToWait = 3;
            thirstyMeow.Play();
            StartCoroutine(WaitForAnimation(secondsToWait));
            print("Cat is meowing (THIRSTY)");
        }
    }

    IEnumerator WaitForAnimation(int seconds) 
    {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        isDoingBehavior = false;
    }

    public Boolean GetHungry()
    {
        return isHungry;
    }

    public void SetHungry(Boolean isHungry)
    {
        if (this.isHungry == isHungry)
            return;
        this.isHungry = isHungry;
        if (isHungry)
        {
            print("Cat is Hungry");
            InvokeRepeating("CatHungryBehavior", 1, hungryMeowInterval);
        }
        else
        {
            print("Cat is no longer hungry");
            CancelInvoke("CatHungryBehavior");
        }
    }


    public Boolean GetThirsty()
    {
        return isThirsty;
    }

    public void SetThirsty(Boolean isThirsty)
    {
        if (this.isThirsty == isThirsty)
            return;
        this.isThirsty = isThirsty;
        if (isThirsty)
        {
            print("Cat is Thirsty");
            InvokeRepeating("CatThirstyBehavior", 1, thirstyMeowInterval);
        }
        else
        {
            print("Cat is no longer thirsty");
            CancelInvoke("CatThirstyBehavior");
        }
    }
}
