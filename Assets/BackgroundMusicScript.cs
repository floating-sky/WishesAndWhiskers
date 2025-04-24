using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    public static BackgroundMusicScript bgMusic;
    public AudioSource music;
    public AudioClip dayMusic;
    public AudioClip nightMusic;

    private void Awake(){
        if(bgMusic != null)
        {
            Destroy(gameObject);
        }
        else
        {
            bgMusic = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void changeMusic(int value)
    {
        switch(value){
            case 0:
                music.resource = dayMusic;
                music.Play();
                break;
            case 1:
                music.resource = nightMusic;
                music.Play();
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
