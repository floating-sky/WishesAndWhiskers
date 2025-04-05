using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatLogicScript : MonoBehaviour
{
    public GameObject dialogueObject;
    public TMP_Text text;
    public GameObject finishWindow;
    public TMP_Text finishText;
    public GameObject controlWindow;
    public GameObject sofaEButton;
    public GameObject catClimbingEButton;
    public GameObject plantEButton;
    public GameObject catBedEButton;
    public GameObject cat;
    [SerializeField] public static int currentLevel = 0;
    [SerializeField] private SofaScript sofa;
    [SerializeField] private CatClimbingScript catClimbing;
    [SerializeField] private PlantScript plant;
    [SerializeField] private CatBedScript catBed;
    [SerializeField] private int currentTasks;
    private int targetTasks;
    private int dialogueInd;
    private int itemNumber;
    private Boolean isAlreadyPlaySofa = false;
    private Boolean isAlreadyPlayCatClimbing = false;
    private Boolean isAlreadyPlant = false;
    private Boolean isDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTasks = 3;
        sofa = GameObject.FindGameObjectWithTag("Sofa").GetComponent<SofaScript>();
        catClimbing = GameObject.FindGameObjectWithTag("CatClimbing").GetComponent<CatClimbingScript>();
        plant = GameObject.FindGameObjectWithTag("Plant").GetComponent<PlantScript>();

        // Setting of Level 2
        if(currentLevel == 2)
        {
            targetTasks = 4;
            sofa.changeView(2);
            finishText.text = "Night 2 complete";
            catBed.gameObject.SetActive(true);
            catBed = GameObject.FindGameObjectWithTag("CatBed").GetComponent<CatBedScript>();
        }

        Time.timeScale = 0f;
        controlWindow.SetActive(true);
        print("current: " + currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Start the game after the player closed the control window
        if(!(Time.timeScale == 0f)){
            // Check does the cat has complete all the tasks
            if(currentTasks >= targetTasks && !isDialogue)
            {
                finishWindow.SetActive(true);
            }
        }

        if(isDialogue && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) )
        {
            print("in dialogue");
            dialogueTextProcess();
            dialogueInd++;
        }

        // Unlock the plant on Night level 2
        if (!isAlreadyPlant && currentLevel == 2)
        {
            plant.setIsReady(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        controlWindow.SetActive(false);
    }

    public void nextLevel()
    {
        // Add care taker level
        print("before change cat level current level: " + LogicScript.currentLevel);
        LogicScript.currentLevel += 1;
        print("after add cat level current level: " + LogicScript.currentLevel);
        SceneManager.LoadScene(1);
    }

    public void CatInteracted()
    {
        print("interacted");

        // Sofa, If cat is in the area of sofa
        if(!isAlreadyPlaySofa && sofa.inTrigger)
        {
            print("Cat play with sofa");

            // Tasks
            currentTasks++;
            isAlreadyPlaySofa = true;
            sofa.setIsAlreadyPlaySofa(true);

            // Dialogues
            dialogueInd = 0;
            isDialogue = true;
            itemNumber = 0;
            Time.timeScale = 0f;
        }
        // Cat Tree, If cat is in the area of cat climbing
        else if(!isAlreadyPlayCatClimbing && catClimbing.inTrigger)
        {
            print("Cat play with cat climbing");

            // Tasks
            currentTasks++;
            isAlreadyPlayCatClimbing = true;
            catClimbing.setIsAlreadyPlayCatClimbing(true);

            // Dialogues
            dialogueInd = 0;
            isDialogue = true;
            itemNumber = 1;
            Time.timeScale = 0f;
        }
        // Plant, After player played sofa and cat tree, it ready to play with plant
        else if (plant.inTrigger && plant.getIsReady() && !isAlreadyPlant)
        {
            print("Cat play with plant");

            // Tasks
            currentTasks++;
            isAlreadyPlant = true;
            plant.setIsAlreadyPlant(true);

            // Dialogues
            dialogueInd = 0;
            isDialogue = true;
            itemNumber = 2;
            Time.timeScale = 0f;
        }
        // Cat bed,
        else if (catBed.inTrigger && catBed.getIsReady() && isAlreadyPlant)
        {
            print("Cat play with cat bed");

            // Tasks
            currentTasks++;
            catBed.setIsAlreadyCatBed(true);

            // Dialogues
            dialogueInd = 0;
            isDialogue = true;
            itemNumber = 3;
            Time.timeScale = 0f;
        }

        // Cat bed lock
        if (currentTasks == 3)
        {
            catBed.setIsReady(true);
        }
    }

    // Text change by mouse clicked
    // Item number 0 = sofa
    // Item number 1 = cat climbing
    // Item number 2 = plant
    // Item number 3 = cat bed
    private void dialogueTextProcess()
    {
        // Level 1 dialogue
        if(currentLevel == 1)
        {
            // sofa
            if(itemNumber == 0)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "Hmm I need to make sure that everyone knows this is MY territory";
                        break;
                    case 1:
                        text.text = "*scratch scratch*";
                        break;
                    case 2:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        sofaEButton.SetActive(false);
                        sofa.changeView(1);

                        // Night Level 1 lock on Plant
                        if (!isAlreadyPlant && currentLevel == 1 && currentTasks == 2 && !isDialogue)
                        {
                            print("Plant unlock");
                            plant.setIsReady(true);
                            plant.OnTriggerEnter2D(cat.GetComponent<BoxCollider2D>());
                        }
                        Time.timeScale = 1f;
                        break;
                }
            }

            // cat climbing
            if(itemNumber == 1)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "I’m now the king of this place!";
                        break;
                    case 1:
                        text.text = "ooo this place is also a really good spot to sleep but it’s too cold right now. I need to find another spot";
                        break;
                    case 2:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        catClimbingEButton.SetActive(false);
                        
                        // Night Level 1 lock on Plant
                        if (!isAlreadyPlant && currentLevel == 1 && currentTasks == 2 && !isDialogue)
                        {
                            print("Plant unlock");
                            plant.setIsReady(true);
                            plant.OnTriggerEnter2D(cat.GetComponent<BoxCollider2D>());
                        }
                        Time.timeScale = 1f;
                        break;
                }
            }

            // plant
            if(itemNumber == 2)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "wow this place feels so warm…. *yawn*";
                        break;
                    case 1:
                        text.text = "Zzzzz....";
                        cat.SetActive(false);
                        plant.changeView(1);                            // Cat sleep on the plant
                        break;
                    case 2:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        plantEButton.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
        }else{
            // sofa
            if(itemNumber == 0)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "Hmmm this sofa smells weird, I don't like it";
                        break;
                    case 1:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        sofaEButton.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }

            // cat climbing
            if(itemNumber == 1)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "I feel kinda bored, I wanna scratch something.";
                        break;
                    case 1:
                        text.text = "Hehe I’ll scratch this";
                        break;
                    case 2:
                        text.text = "*scratch scratch*";
                        break;
                    case 3:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        catClimbingEButton.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }

            // plant
            if(itemNumber == 2)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "Maybe I can sleep here again Meow!";
                        break;
                    case 1:
                        text.text = "Wait, if I sleep here...is that thing going to happen again!?";
                        break;
                    case 2:
                        text.text = "That was a nightmare...";
                        break;
                    case 3:
                        text.text = "Maybe I should find another place to sleep meow...";
                        break;
                    case 4:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        plantEButton.SetActive(false);
                        catBed.OnTriggerEnter2D(cat.GetComponent<BoxCollider2D>());
                        Time.timeScale = 1f;
                        break;
                }
            }

            // cat bed
             if(itemNumber == 3)
            {
                switch(dialogueInd)
                {
                    case 0:
                        dialogueObject.SetActive(true);
                        text.text = "I think they must’ve gotten this bed for me to sleep in!";
                        break;
                    case 1:
                        text.text = "Yay now I have a nice warm place to sleep";
                        break;
                    case 2:
                        isDialogue = false;
                        dialogueObject.SetActive(false);
                        catBedEButton.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
        }
    }
}
