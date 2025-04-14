using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Microsoft.Unity.VisualStudio.Editor;

public class CanvasScript : MonoBehaviour
{
    public TMP_Text text;
    private int sceneCounter = 0;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject Scene;
    [SerializeField] private GameObject Scene3;
    [SerializeField] private Sprite scene1;

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
        switch(sceneCounter){
            case 1:
                Scene.GetComponent<SpriteRenderer>().sprite = scene1;
                break;
            case 2:
                Scene.SetActive(false);
                Scene3.SetActive(true);
                dialogue.SetActive(true);
                break;
            case 3:
                text.text = "Aww don’t be scared Samm, \nI’ll take good care of you :)";
                break;
        }
    }

    public void endScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
