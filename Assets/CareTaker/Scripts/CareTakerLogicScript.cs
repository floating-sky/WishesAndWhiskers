using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public TimelineAsset level1;
    public TimelineAsset level2;

    public CatScript[] Cats;
    public GameObject controlWindow;
    public GameObject finishWindow;
    public TMP_Text finishText;
    public GameObject spongeWindow;
    public GameObject newMaterialWindow;
    public GameObject sponge;
    public GameObject spongeInBath;
    public GameObject catInBath;
    public PlayableDirector timeline;
    [SerializeField] public static int currentLevel = 1;
    private BarScript Bar;
    public SofaScriptCarer sofa;
    public Boolean isBath = false;
    [SerializeField] private BarScript WashBar;
    private int feedCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setting
        int ind = 0;
        feedCount = 0;
        timeline.playableAsset = level1;                    // Assign the time line

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cat");
        Cats = new CatScript[gameObjects.Length];
        Bar = GameObject.FindGameObjectWithTag("Bar").GetComponent<BarScript>();

        // Getting all the cats
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cat"))
        {
            Cats[ind] = gameObject.GetComponent<CatScript>();
            ind++;
        }

        // Show up control window
        Time.timeScale = 0f;
        controlWindow.SetActive(true);

        // Setting of Level 2
        if (currentLevel == 2)
        {
            sponge.SetActive(true);
            timeline.playableAsset = level2;
            sofa.ChangeView(1);
            Cats[0].isDirty = true;
        }

        // If is level then start the timeline immediately
        if(currentLevel == 1){
            timeline.Play();
        }
        print("currentLevel: " + currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Next Level
        if (Bar.getValue() >= 1.0)
        {
            // Change text message
            switch(currentLevel)
            {
                case 1:
                    finishText.text = "Day 1 complete!";
                    break;
                case 2:
                    finishText.text = "Day 2 complete!";
                    break;
            }

            finishWindow.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (Bar.getValue() < 0.0 && Bar.isFeedTwice)
        {
            print("Level 1 fail");
            Time.timeScale = 0.0f;
            Application.Quit();
        }

        // Check all the cats meow is up to 2 then decrease the process bar
        CheckCatsMeowCount();

        // Bath time
        if (isBath)
        {
            // Check wash bar is full or not
            if (WashBar.getValue() >= 1.0)
            {
                spongeLogicEnd();
            }
        }
    }

    public void SetCatHungry()
    {
        int catInd = UnityEngine.Random.Range(0, Cats.Length);

        // If cat is not hungry, set it to hungry
        if (!Cats[catInd].GetHungry())
        {
            Cats[catInd].SetHungry(true);
        }
    }

    public void SetCatThirsty()
    {
        int catInd = UnityEngine.Random.Range(0, Cats.Length);

        // If cat is not thirsty, set it to thirsty
        if (!Cats[catInd].GetThirsty())
        {
            Cats[catInd].SetThirsty(true);
        }
    }

    public void SetCatWantsPlay()
    {
        int catInd = UnityEngine.Random.Range(0, Cats.Length);

        // If cat is not thirsty, set it to thirsty
        if (!Cats[catInd].wantsPlay)
        {
            Cats[catInd].SetWantsPlay(true);
        }
    }

    public void CatPlayed() 
    {
        Bar.IncreaseProgressBar(0.2f);
    }

    public void CatAteFoodORWater()
    {
        feedCount++;
        if (feedCount >= 2)
        {
            Bar.isFeedTwice = true;
        }
        Bar.IncreaseProgressBar(0.2f);
    }

    public void NextLevelButton()
    {
        CatLogicScript.currentLevel += 1;
        SceneManager.LoadSceneAsync(2);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        controlWindow.SetActive(false);
    }

    public void spongeLogic()
    {
        spongeWindow.SetActive(true);
        spongeInBath.SetActive(true);
        catInBath.SetActive(true);
        isBath = true;
    }

    public void spongeLogicEnd()
    {
        spongeWindow.SetActive(false);
        spongeInBath.SetActive(false);
        catInBath.SetActive(false);
        newMaterialWindow.SetActive(true);
        Bar.IncreaseProgressBar(0.2f);
        isBath = false;
        Cats[0].isDirty = false;
    }

    public void newMaterialWindowLogic()
    {
        newMaterialWindow.SetActive(false);
        sofa.ChangeView(2);
        timeline.Play();
    }

    private void CheckCatsMeowCount()
    {
        foreach (CatScript cat in Cats)
        {
            if (cat.meowCount >= 2 && !(Time.timeScale == 0f))
            {
                Bar.DecreaseProgressBar(0.01f);
            }
        }
    }
}
