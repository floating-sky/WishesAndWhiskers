using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public float increaseSpeed;
    public float decreaseSpeed;
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
}
