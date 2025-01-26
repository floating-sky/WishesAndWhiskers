using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public CatScript[] Cats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ind = 0;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cat");
        Cats = new CatScript[gameObjects.Length];
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Cat")){
            Cats[ind] = gameObject.GetComponent<CatScript>();
            ind++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
