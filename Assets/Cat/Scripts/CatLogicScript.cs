using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatLogicScript : MonoBehaviour
{
    public GameObject dialogueObject;
    public TMP_Text text;
    public GameObject finishWindow;
    [SerializeField] private SofaScript sofa;
    [SerializeField] private CatClimbingScript catClimbing;
    [SerializeField] private int currentTasks;
    private int targetTasks;
    private int countMouseClicked;
    private int itemNumber;
    private Boolean isAlreadyPlaySofa = false;
    private Boolean isAlreadyPlayCatClimbing = false;
    private Boolean isDialogue = false;

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
        if(currentTasks >= targetTasks && !isDialogue)
        {
            finishWindow.SetActive(true);
        }

        if(Input.GetMouseButtonDown(0))
        {
            countMouseClicked++;
        }
        
        if(isDialogue)
        {
            dialogueTextProcess(itemNumber, countMouseClicked);
        }
    }

    public void nextLevel()
    {
        Application.Quit();
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
            countMouseClicked = 0;
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
            countMouseClicked = 0;
            isDialogue = true;
            itemNumber = 1;
            Time.timeScale = 0f;
        }
    }

    // Text change by mouse clicked
    // Item number 0 = sofa
    // Item number 1 = cat climbing
    private void dialogueTextProcess(int itemNumber, int mouseClicked)
    {
        // sofa
        if(itemNumber == 0)
        {
            switch(mouseClicked)
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
                    Time.timeScale = 1f;
                    break;
            }
        }

        // cat climbing
        if(itemNumber == 1)
        {
            switch(mouseClicked)
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
                    Time.timeScale = 1f;
                    break;
            }
        }
    }
}
