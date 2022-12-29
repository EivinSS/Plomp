using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource _audio;
    [SerializeField] List<SceneNameAudioClip> _sceneNameAudioClipList = new List<SceneNameAudioClip>();

    [Serializable]
    class SceneNameAudioClip
    {
        public string SceneName;
        public AudioClip AudioClip;
    }

    void Start()
    {
        int numMusicManagers = FindObjectsOfType<MusicManager>().Length;
        if(numMusicManagers > 1)
        {
            Destroy(this.gameObject);
        }

        SceneManager.activeSceneChanged += SceneChange;
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.GetInt(NameConfig.music) == 0)
        {
            return;
        }

        if (!_audio.isPlaying)
        {
            PlayCurrentSceneSong(); 
        }
    }

    public void PlayCurrentSceneSong()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        foreach (SceneNameAudioClip sceneNameAudioClip in _sceneNameAudioClipList) {
            if(sceneNameAudioClip.SceneName == currentSceneName)
            {
                Debug.Log(currentSceneName);
                _audio.clip = sceneNameAudioClip.AudioClip;
                _audio.Play();
                return;
            }
        }
        Debug.Log("Did not find a song");
    }

    AudioClip GetAudioClipOfScene(String sceneName)
    {
        foreach (SceneNameAudioClip sceneNameAudioClip in _sceneNameAudioClipList)
        {
            if (sceneNameAudioClip.SceneName == sceneName)
            {
                Debug.Log("currentSceneName");
                return sceneNameAudioClip.AudioClip;
            }
        }
        return null;
    }

    void SceneChange(Scene current, Scene next)
    {

        if (PlayerPrefs.GetInt(NameConfig.music) == 0)
        {
            return;
        }

            if (_audio != null && _audio.isPlaying)
        {
            AudioClip previousClip = _audio.clip;
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log(currentSceneName);
            AudioClip nextClip = GetAudioClipOfScene(currentSceneName);

            if(previousClip == nextClip)
            {
                Debug.Log("same song");
                return;
            }
            else
            {
                _audio.clip = nextClip;
                _audio.Play();
            }
        }
        else if(_audio != null && !_audio.isPlaying)
        {
            _audio.clip = GetAudioClipOfScene(SceneManager.GetActiveScene().name);
            _audio.Play();
        }
    }

    public void StopMusic()
    {
        _audio.Stop();
    }
}
