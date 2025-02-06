using Unity.VisualScripting;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public CatScript[] Cats;
    private BarScript Bar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ind = 0;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cat");
        Cats = new CatScript[gameObjects.Length];
        Bar = GameObject.FindGameObjectWithTag("Bar").GetComponent<BarScript>();

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Cat")){
            Cats[ind] = gameObject.GetComponent<CatScript>();
            ind++;
        }

        // Check all the cats meow is up to 2 then decrease the process bar
        CheckCatsMeowCount();

    }

    // Update is called once per frame
    void Update()
    {
        if(Bar.getValue() > 1.0)
        {
            print("Level 1 passed!");
        }
    }

    public void SetCatHungry()
    {
        int catInd = Random.Range(0, Cats.Length);
        Cats[catInd].SetHungry(true);
    }

    public void SetCatThirsty()
    {
        int catInd = Random.Range(0, Cats.Length);
        Cats[catInd].SetThirsty(true);
    }

    private void CheckCatsMeowCount()
    {
        foreach(CatScript cat in Cats){
            if(cat.meowCount >= 2)
            {
                print("feed you cat!");
                Bar.DecreaseProgressBar(1);
            }
        }
    }
}
