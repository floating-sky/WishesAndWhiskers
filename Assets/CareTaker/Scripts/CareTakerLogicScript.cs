using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public CatScript[] Cats;
    public GameObject controlWindow;
    private BarScript Bar;
    private int feedCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ind = 0;
        feedCount = 0;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cat");
        Cats = new CatScript[gameObjects.Length];
        Bar = GameObject.FindGameObjectWithTag("Bar").GetComponent<BarScript>();

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cat")){
            Cats[ind] = gameObject.GetComponent<CatScript>();
            ind++;
        }

        Time.timeScale = 0f;
        controlWindow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Bar.getValue() > 1.0)
        {
            print("Level 1 passed!");
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

    public void Resume()
    {
        Time.timeScale = 1f;
        controlWindow.SetActive(false);
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
