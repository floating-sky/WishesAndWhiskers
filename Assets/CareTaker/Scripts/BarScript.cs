using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public float increaseSpeed;
    public float decreaseSpeed;
    public GameObject finishWindow;
    public Boolean isFeedTwice;                            // After the feed the cat twice, fail statement apply
    private Slider bar;
    [SerializeField] private float targetProcess;

    private void Awake()
    {
        bar = gameObject.GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        increaseSpeed = 0.4f;
        decreaseSpeed = 0.05f;
        isFeedTwice = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bar.value < targetProcess)
        {
            bar.value += increaseSpeed * Time.deltaTime;
        }

        if (bar.value > targetProcess)
        {
            bar.value -= decreaseSpeed * Time.deltaTime;
        }

        // Next Level
        if(bar.value == 1)
        {
            finishWindow.SetActive(true);
        }
        else
        {
            finishWindow.SetActive(false);
        }

        // Fail Statement
        if(isFeedTwice && bar.value == 0){
            print("Gameover");
            Application.Quit();
        }
    }

    public void IncreaseProgressBar(float value)
    {
        targetProcess = bar.value;
        targetProcess += bar.value + value;
    }

    public void DecreaseProgressBar(float value)
    {
        targetProcess = bar.value;
        targetProcess -= bar.value + value;
    }

    public float getValue()
    {
        return bar.value;
    }

    // Next Level Button Function
    public void nextLevel(){
        // Right now just end the game
        Application.Quit();
    }
}
