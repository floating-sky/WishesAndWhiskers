using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class CatScript : MonoBehaviour
{

    [SerializeField] public AudioSource hungryMeow;
    [SerializeField] public AudioSource thirstyMeow;

    [SerializeField] GameObject bowls;
    [SerializeField] GameObject plant;

    public ToyScript toyScript;

    private Boolean isWalkingToFoodBowl = false;
    private Boolean isWalkingToWaterBowl = false;
    private Boolean isWalkingToPlant = false;
    public Boolean isDirty = false;
    public Boolean isHungry = false;
    public Boolean isThirsty = false;
    public Boolean wantsPlay = false;
    public Boolean wantsSleepInPlant = false;
    public int meowCount = 0;
    public Vector3 lastPos;
    public LogicScript logicScript;
    private Boolean inTrigger = false;              // Is anything on the cat
    private Collider2D inputObject;                 // the object on the cat
    private bool bowlIsDest;

    // Cat is busy when they are doing an action that should not be interrupted
    public Boolean isBusy = false;

    // Cat is expressing a need through behavior. Used so that random movement doesn't interupt cat during behavior animation, but other actions CAN interupt
    public Boolean isDoingBehavior = false;

    public int moveRadius = 6; // How far from their current position the cat will move when they move to a random point
    public float hungryMeowInterval; // Seconds between each meow the cat makes when they are hungry
    public float thirstyMeowInterval;
    public float wantsPlayInterval;
    public float randomMovementInterval; // Seconds between each time the cat moves to a random location
    public float wantsSleepInPlantInterval;

    private Vector2 velocity;

    NavMeshAgent agent;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        InvokeRepeating("MoveCatToRandomDestination", 2f, randomMovementInterval);
        logicScript = GameObject.FindGameObjectWithTag("CareTakerLogic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((isWalkingToFoodBowl || isWalkingToWaterBowl) && bowlIsDest && Vector3.Distance(transform.position, agent.destination) <= 1)
        {
            bowlIsDest = false;
            print("REACHED DESTINATION");
            agent.isStopped = true;
            animator.SetBool("isEatingDrinking", true);
            StartCoroutine(WaitForEatingDrinkingAnimation(4f));
        }

        if (isWalkingToPlant && Vector3.Distance(transform.position, agent.destination) <= 1)
        {
            GoInsidePlant();
        }
    }

    private void FixedUpdate()
    {
        if (inTrigger)
        {
            // toy
            if (inputObject.gameObject.layer == 9)
            {
                toyScript.changeMovingToy();
                if (Input.GetMouseButtonUp(0) && wantsPlay)                     // if mouse released and bowl doesn't have food, add food 
                {
                    Play();
                }
            }
        }
        else
        {
            toyScript.changeToy();
        }
    }

    private void LateUpdate()
    {
        if (transform.position != lastPos)
        {
            if (lastPos.x > transform.position.x)
            {
                animator.SetBool("isWalkingLeft", true);
                animator.SetBool("isWalkingRight", false);
            }
            else
            {
                animator.SetBool("isWalkingRight", true);
                animator.SetBool("isWalkingLeft", false);
            }

        }
        else
        {
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingRight", false);
        }
        lastPos = transform.position;
    }

    IEnumerator WaitForEatingDrinkingAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        isBusy = false;
        animator.SetBool("isEatingDrinking", false);
        if (isWalkingToFoodBowl)
        {
            isWalkingToFoodBowl = false;
            SetHungry(false);
            bowls.GetComponent<BowlsScript>().SetFood(false);
        }
        else if (isWalkingToWaterBowl)
        {
            isWalkingToWaterBowl = false;
            SetThirsty(false);
            bowls.GetComponent<BowlsScript>().SetWater(false);
        }
    }

    IEnumerator WaitForPlayingAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        isBusy = false;
        isDoingBehavior = false;
        animator.SetBool("isPlaying", false);
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
            print("MOVING CAT");
            agent.SetDestination(GetRandomNavmeshLocation(moveRadius));
        }
    }

    public void CatHungryBehavior()
    {
        if (!isBusy && !isDoingBehavior)
        {
            isDoingBehavior = true;
            agent.isStopped = true;
            float secondsToWait = 1.5f;
            hungryMeow.Play();
            animator.SetBool("isMeowing", true);
            StartCoroutine(WaitForAnimation(secondsToWait, "isMeowing"));
            meowCount++;
            print("Cat is meowing (HUNGRY)");
        }
    }

    public void CatThirstyBehavior()
    {
        if (!isBusy && !isDoingBehavior)
        {
            isDoingBehavior = true;
            agent.isStopped = true;
            float secondsToWait = 1.5f;
            thirstyMeow.Play();
            animator.SetBool("isMeowing", true);
            StartCoroutine(WaitForAnimation(secondsToWait, "isMeowing"));
            meowCount++;
            print("Cat is meowing (THIRSTY)");
        }
    }

    public void CatWantsPlayBehavior()
    {
        if (!isBusy && !isDoingBehavior)
        {
            isDoingBehavior = true;
            agent.isStopped = true;
            //float secondsToWait = 2f;
            animator.SetBool("isWantingPlay", true);
            // StartCoroutine(WaitForAnimation(secondsToWait, "isWantingPlay"));
            print("Cat wants to play");
        }
    }

    public void CatWantsSleepInPlantBehavior()
    {
        if (!isBusy && !isDoingBehavior) 
        {
            isDoingBehavior = true;
            isBusy = true;
            isWalkingToPlant = true;
            agent.SetDestination(plant.transform.position);
        }
    }

    public void GoInsidePlant() 
    {
        print("going inside plant");
        isWalkingToPlant = false;
        isDoingBehavior = true;
        isBusy = true;
        agent.isStopped = true;
        GetComponent<SpriteRenderer>().enabled = false;
        plant.GetComponent<CaretakerPlantScript>().CatGoesInside();
        SetWantsSleepInPlant(false);
    }

    IEnumerator WaitForAnimation(float seconds, string animationBool)
    {
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        isDoingBehavior = false;
        animator.SetBool(animationBool, false);
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

    public void SetWantsPlay(Boolean wantsPlay)
    {
        if (this.wantsPlay == wantsPlay)
            return;
        this.wantsPlay = wantsPlay;
        if (wantsPlay)
        {
            print("Cat wants to play");
            InvokeRepeating("CatWantsPlayBehavior", 1, wantsPlayInterval);
        }
        else
        {
            print("Cat no longer wants to play");
            CancelInvoke("CatWantsPlayBehavior");
        }
    }

    public void SetWantsSleepInPlant(Boolean wantsSleepInPlant)
    {
        if (this.wantsSleepInPlant == wantsSleepInPlant)
            return;
        this.wantsSleepInPlant = wantsSleepInPlant;
        if (wantsSleepInPlant)
        {
            print("Cat wants to sleep in the plant");
            InvokeRepeating("CatWantsSleepInPlantBehavior", 1, wantsSleepInPlantInterval);
        }
        else
        {
            print("Cat no longer wants to sleep in the plant");
            CancelInvoke("CatWantsSleepInPlantBehavior");
        }
    }

    public void CatCleaned() 
    {
        isBusy = false;
        agent.isStopped = false;
        isDoingBehavior = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EatFood()
    {
        if (!isBusy && isHungry)
        {
            isBusy = true;
            isWalkingToFoodBowl = true;
            Vector3 pos = bowls.transform.position;
            pos.x = pos.x - .5f;
            agent.SetDestination(pos);
            bowlIsDest = true;
            meowCount = 0;
        }
    }

    public void DrinkWater()
    {
        if (!isBusy && isThirsty)
        {
            isBusy = true;
            isWalkingToWaterBowl = true;
            Vector3 pos = bowls.transform.position;
            pos.x = pos.x + .5f;
            agent.SetDestination(pos);
            bowlIsDest = true;
            meowCount = 0;
        }
    }

    public void Play() 
    {
        if (wantsPlay)
        {
            isBusy = true;
            agent.isStopped = true;
            float secondsToWait = 4f;
            animator.SetBool("isWantingPlay", false);
            animator.SetBool("isPlaying", true);
            StartCoroutine(WaitForPlayingAnimation(secondsToWait));
            print("cat is playing!");
            SetWantsPlay(false);
            logicScript.CatPlayed();
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
