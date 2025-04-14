using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Day2CutSceneScipt : MonoBehaviour
{
    public TMP_Text text;
    private int sceneCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextScene()
    {
        sceneCounter++;
        switch(sceneCounter)
        {
            case 1:
                text.text = "But I would probably need to find some sort of anti cat spray or something to make sure that the new one doesn’t get scratched up";
                break;
        }
    }

    public void endScene()
    {
        SceneManager.UnloadSceneAsync(4);
        LogicScript.isCutSceneOver = true;
    }
}
