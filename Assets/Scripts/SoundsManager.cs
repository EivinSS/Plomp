using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourcePlayerMovement;

    [SerializeField] AudioClip playerMove;

    [SerializeField] AudioSource audioSourceOtherSounds;

    [SerializeField] AudioClip blockMove;
    [SerializeField] AudioClip iceCrack;
    [SerializeField] AudioClip winning;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip activateGoal;

    public void PlayPlayerMoveSound(GameManager.SoundEnum soundEnum)
    {
        if (PlayerPrefs.GetInt(NameConfig.sound) == 0)
        {
            return;
        }

        if (soundEnum == GameManager.SoundEnum.PlayerMove)
        {
            audioSourcePlayerMovement.clip = playerMove;
            audioSourcePlayerMovement.Play();
        }
    }

    public void PlaySound(GameManager.SoundEnum soundEnum)
    {
        if (PlayerPrefs.GetInt(NameConfig.sound) == 0)
        {
            return;
        }

        if (soundEnum == GameManager.SoundEnum.BlockMove)
        {
            audioSourceOtherSounds.clip = blockMove;

        }
        if(soundEnum == GameManager.SoundEnum.IceCrack)
        {
            audioSourceOtherSounds.clip = iceCrack;

        }
        if(soundEnum == GameManager.SoundEnum.Winning)
        {
            audioSourceOtherSounds.clip = winning;

        }
        if(soundEnum == GameManager.SoundEnum.Coin)
        {
            audioSourceOtherSounds.clip = coin;
        }
        if(soundEnum == GameManager.SoundEnum.Death)
        {
            audioSourceOtherSounds.clip = death;
            
        }if(soundEnum == GameManager.SoundEnum.ActivateGoal)
        {
            audioSourceOtherSounds.clip = activateGoal;
            
        }
        if(audioSourceOtherSounds.clip != null)
        {
            audioSourceOtherSounds.Play();
        }   
    }
}
