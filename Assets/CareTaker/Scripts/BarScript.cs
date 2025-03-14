using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public float increaseSpeed;
    public float decreaseSpeed;
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
        if (bar.value <= targetProcess)
        {
            bar.value += increaseSpeed * Time.deltaTime;
        }

        if (bar.value > targetProcess)
        {
            bar.value -= decreaseSpeed * Time.deltaTime;
        }
    }

    public void IncreaseProgressBar(float value)
    {
        targetProcess += value;
    }

    public void DecreaseProgressBar(float value)
    {
        if(isFeedTwice)
        {
            targetProcess -= value;
            if(targetProcess < 0)
            {
                targetProcess = 0;
            }
        }
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
