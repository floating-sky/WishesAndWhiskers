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
    public GameObject controlWindow;
    public GameObject sofaEButton;
    public GameObject catClimbingEButton;
    public GameObject plantEButton;
    public GameObject cat;
    [SerializeField] public static int currentLevel = 0;
    [SerializeField] private SofaScript sofa;
    [SerializeField] private CatClimbingScript catClimbing;
    [SerializeField] private PlantScript plant;
    [SerializeField] private int currentTasks;
    private int targetTasks;
    private int dialogueInd;
    private int itemNumber;
    private Boolean isAlreadyPlaySofa = false;
    private Boolean isAlreadyPlayCatClimbing = false;
    private Boolean isDialogue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTasks = 3;
        sofa = GameObject.FindGameObjectWithTag("Sofa").GetComponent<SofaScript>();
        catClimbing = GameObject.FindGameObjectWithTag("CatClimbing").GetComponent<CatClimbingScript>();
        plant = GameObject.FindGameObjectWithTag("Plant").GetComponent<PlantScript>();

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
            dialogueTextProcess();
            dialogueInd++;
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
        SceneManager.LoadSceneAsync(1);
    }

    public void CatInteracted()
    {
        // If cat is in the area of sofa
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

        // If cat is in the area of cat climbing
        if(!isAlreadyPlayCatClimbing && catClimbing.inTrigger)
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

        // After player played sofa and cat tree, it ready to play with plant
        if(currentTasks == 2)
        {
            plant.setIsReady(true);
        }

        if(plant.inTrigger && (currentTasks == 2))
        {
            print("Cat play with plant");

            // Tasks
            currentTasks++;

            // Dialogues
            dialogueInd = 0;
            isDialogue = true;
            itemNumber = 2;
            Time.timeScale = 0f;
        }
    }

    // Text change by mouse clicked
    // Item number 0 = sofa
    // Item number 1 = cat climbing
    // Item number 2 = plant
    private void dialogueTextProcess()
    {
        // sofa
        if(itemNumber == 0)
        {
            switch(dialogueInd)
            {
                case 0:
                    dialogueObject.SetActive(true);
                    text.text = "Looks really soft, I’m tempted to scratch it. Maybe the cat tree would be better. Meow.";
                    break;
                case 1:
                    text.text = "Looks really soft, I’m tempted to scratch it. I really want to starch something. Meow.";
                    break;
                case 2:
                    text.text = "*scratch scratch scratch*";
                    break;
                case 3:
                    isDialogue = false;
                    dialogueObject.SetActive(false);
                    sofaEButton.SetActive(false);
                    sofa.changeView(1);
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
                    text.text = "This so meowvelous. Yum";
                    break;
                case 1:
                    text.text = "*scratch scratch scratch*";
                    break;
                case 2:
                    text.text = "I'm the king of the house now";
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
                    text.text = "I'm tired meow~";
                    break;
                case 1:
                    text.text = "This place looks warm and safe";
                    break;
                case 2:
                    text.text = "Zzzzz....";
                    cat.SetActive(false);
                    plant.changeView(1);                            // Cat sleep on the plant
                    break;
                case 3:
                    isDialogue = false;
                    dialogueObject.SetActive(false);
                    plantEButton.SetActive(false);
                    Time.timeScale = 1f;
                    break;
            }
        }
    }
}
