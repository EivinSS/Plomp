using UnityEngine;
using UnityEngine.UI;

public class MusicAndSoundToggle : MonoBehaviour
{
    [SerializeField] GameEvent on;
    [SerializeField] GameEvent off;

    [SerializeField] Button musicToggle;
    [SerializeField] Button soundToggle;
    [SerializeField] Image musicImage;
    [SerializeField] Image soundImage;


    public bool isMusic;
    public bool isSound;

    private void Start()
    {
        if(isMusic)
        {
            if (PlayerPrefs.GetInt(NameConfig.music) == 1)
            {
                Color myColor = musicImage.color;
                myColor.a = 1f;
                musicImage.color = myColor;
            }
            else if (PlayerPrefs.GetInt(NameConfig.music) == 0)
            {
                Color myColor = musicImage.color;
                myColor.a = 0.5f;
                musicImage.color = myColor;
            }
        }
        
        if(isSound)
        {
            if (PlayerPrefs.GetInt(NameConfig.sound) == 1)
            {
                Color myColor = soundImage.color;
                myColor.a = 1f;
                soundImage.color = myColor;
            }
            else if (PlayerPrefs.GetInt(NameConfig.sound) == 0)
            {
                Color myColor = soundImage.color;
                myColor.a = 0.5f;
                soundImage.color = myColor;
            }
        }
    }

    public void UserToggleMusic()
    {
        bool toggleWasOff = PlayerPrefs.GetInt(NameConfig.music) == 0;
        if (toggleWasOff)
        {
            on.Raise();
            PlayerPrefs.SetInt(NameConfig.music, 1);
            Start();
        }
        if(!toggleWasOff)
        {
            off.Raise();
            PlayerPrefs.SetInt(NameConfig.music, 0);
            Start();
        }
    }

    public void UserToggleSound()
    {
        bool toggleWasOff = PlayerPrefs.GetInt(NameConfig.sound) == 0;
        if (toggleWasOff)
        {
            on.Raise();
            PlayerPrefs.SetInt(NameConfig.sound, 1);
            Start();
        }
        if (!toggleWasOff)
        {
            off.Raise();
            PlayerPrefs.SetInt(NameConfig.sound, 0);
            Start();
        }
    }
}
