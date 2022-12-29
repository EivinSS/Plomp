using UnityEngine;
using UnityEngine.UI;

public class MusicAndSoundToggle : MonoBehaviour
{
    [SerializeField] GameEvent on;
    [SerializeField] GameEvent off;

    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;

    public bool isMusic;
    public bool isSound;

    private void Start()
    {
        if(isMusic)
        {
            if (PlayerPrefs.GetInt(NameConfig.music) == 1)
            {
                musicToggle.isOn = true;
            }
            else if (PlayerPrefs.GetInt(NameConfig.music) == 0)
            {
                musicToggle.isOn = false;
            }
        }
        
        if(isSound)
        {
            if (PlayerPrefs.GetInt(NameConfig.sound) == 1)
            {
                soundToggle.isOn = true;
            }
            else if (PlayerPrefs.GetInt(NameConfig.sound) == 0)
            {
                soundToggle.isOn = false;
            }
        }
    }

    public void UserToggleMusic(bool toggle)
    {
        if(toggle)
        {
            if(PlayerPrefs.GetInt(NameConfig.music) == 1)
            {
                return;
            }
            on.Raise();
            PlayerPrefs.SetInt(NameConfig.music, 1);
        }
        if(!toggle)
        {
            if(PlayerPrefs.GetInt(NameConfig.music) == 0)
            {
                return;
            }
            off.Raise();
            PlayerPrefs.SetInt(NameConfig.music, 0);
        }
    }

    public void UserToggleSound(bool toggle)
    {
        if (toggle)
        {
            if (PlayerPrefs.GetInt(NameConfig.sound) == 1)
            {
                return;
            }
            on.Raise();
            PlayerPrefs.SetInt(NameConfig.sound, 1);
        }
        if (!toggle)
        {
            if (PlayerPrefs.GetInt(NameConfig.sound) == 0)
            {
                return;
            }
            off.Raise();
            PlayerPrefs.SetInt(NameConfig.sound, 0);
        }
    }
}
