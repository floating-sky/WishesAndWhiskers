using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public float increaseSpeed;
    public float decreaseSpeed;
    public Boolean isFeedTwice;                            // After the feed the cat twice, fail statement apply
    private Slider bar;
    private float difference;
    private float threshold = 0.005f;
    [SerializeField] private float targetProcess;

    private void Awake()
    {
        bar = gameObject.GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        increaseSpeed = 0.2f;
        decreaseSpeed = 0.05f;
        isFeedTwice = false;
    }

    // Update is called once per frame
    void Update()
    {
        difference = Mathf.Abs(bar.value - targetProcess);

        if(difference > threshold){
            if (bar.value < targetProcess)
            {
                bar.value += increaseSpeed * Time.deltaTime;
            }
            else if (bar.value > targetProcess)
            {
                bar.value -= decreaseSpeed * Time.deltaTime;
            }
        }
        else
        {
            bar.value = targetProcess;
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
}
