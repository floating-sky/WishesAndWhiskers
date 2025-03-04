using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class LogicScript : MonoBehaviour
{
    public TimelineAsset level1;
    public TimelineAsset level2;

    public CatScript[] Cats;
    public GameObject controlWindow;
    public GameObject finishWindow;
    public GameObject spongeWindow;
    public GameObject Sponge;
    public PlayableDirector timeline;
    [SerializeField] public static int currentLevel = 1;
    private BarScript Bar;
    private int feedCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Setting
        int ind = 0;
        feedCount = 0;
        timeline.playableAsset = level1;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cat");
        Cats = new CatScript[gameObjects.Length];
        Bar = GameObject.FindGameObjectWithTag("Bar").GetComponent<BarScript>();

        // Getting all the cats
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cat")){
            Cats[ind] = gameObject.GetComponent<CatScript>();
            ind++;
        }

        // Show up control window
        Time.timeScale = 0f;
        controlWindow.SetActive(true);

        // Setting of Level 2
        if(currentLevel == 2)
        {
            Sponge.SetActive(true);
            timeline.playableAsset = level2;
        }

        print("currentLevel: " + currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Next Level
        if(Bar.getValue() >= 1.0)
        {
            print("Level 1 passed!");
            finishWindow.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if(Bar.getValue() < 0.0 && Bar.isFeedTwice)
        {
            print("Level 1 fail");
            Time.timeScale = 0.0f;
            Application.Quit();
        }

        // Check all the cats meow is up to 2 then decrease the process bar
        CheckCatsMeowCount();
    }

    public void SetCatHungry()
    {
        int catInd = Random.Range(0, Cats.Length);

        // If cat is not hungry, set it to hungry
        if(!Cats[catInd].GetHungry())
        {
            Cats[catInd].SetHungry(true);
        }
    }

    public void SetCatThirsty()
    {
        int catInd = Random.Range(0, Cats.Length);

        // If cat is not thirsty, set it to thirsty
        if(!Cats[catInd].GetThirsty())
        {
            Cats[catInd].SetThirsty(true);
        }
    }

    public void CatAteFoodORWater()
    {
        feedCount++;
        if(feedCount >= 2){
            Bar.isFeedTwice = true;
        }
        Bar.IncreaseProgressBar(0.2f);
    }

    public void NextLevelButton()
    {
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
    }

    private void CheckCatsMeowCount()
    {
        foreach(CatScript cat in Cats){
            if(cat.meowCount >= 2)
            {
                Bar.DecreaseProgressBar(0.01f);
            }
        }
    }
}
