using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    [SerializeField] Slider soundSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSound()
    {
        AudioListener.volume = soundSlider.value;
        Save();
    }

    private void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", soundSlider.value);
    }
}
