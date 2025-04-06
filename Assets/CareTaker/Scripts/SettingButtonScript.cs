using UnityEngine;

public class SettingScript : MonoBehaviour
{
    public GameObject settingWindow;
    public SettingWindowScript settingWindowScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppearSettingWindow()
    {
        settingWindowScript.setIsSettingWindow(true);
        settingWindow.SetActive(true);
        Time.timeScale = 0f;
    }
}
