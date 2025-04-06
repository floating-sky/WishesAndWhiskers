using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingWindowScript : MonoBehaviour
{
    public GameObject settingWindow;
    public GameObject controlWindow;
    private Boolean isSettingWindow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean getIsSettingWindow()
    {
        return isSettingWindow;
    }

    public void setIsSettingWindow(Boolean value)
    {
        isSettingWindow = value;
    }

    public void Resume()
    {
        settingWindow.SetActive(false);
        isSettingWindow = false;
        Time.timeScale = 1f;
    }

    public void Control()
    {
        controlWindow.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
