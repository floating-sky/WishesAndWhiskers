using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public float FillSpeed;
    private Slider bar;
    private float targetProcess;

    private void Awake()
    {
        bar = gameObject.GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (bar.value > targetProcess)
        {
            bar.value -= FillSpeed * Time.deltaTime;
        }
    }

    public void DecreaseProgressBar(float value)
    {
        targetProcess -= bar.value + value;
    }
}
