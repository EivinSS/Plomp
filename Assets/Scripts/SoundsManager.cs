using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourcePlayerMovement;

    [SerializeField] AudioClip playerMove;

    [SerializeField] AudioSource audioSourceBlockMove;
    [SerializeField] AudioSource audioSourceIceCrack;
    [SerializeField] AudioSource audioSourceWinning;
    [SerializeField] AudioSource audioSourceCoin;
    [SerializeField] AudioSource audioSourceDeath;
    [SerializeField] AudioSource audioSourceActivateGoal;

    [SerializeField] AudioClip blockMove;
    [SerializeField] AudioClip iceCrack;
    [SerializeField] AudioClip winning;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip activateGoal;

    public void PlayPlayerMoveSound(GameManager.SoundEnum soundEnum)
    {

        if (!PlayerPrefs.HasKey(NameConfig.sound))
        {
            PlayerPrefs.SetInt(NameConfig.sound, 1);
        }

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
            audioSourceBlockMove.clip = blockMove;
            audioSourceBlockMove.volume = 0.8f;
            audioSourceBlockMove.Play();

        }
        if(soundEnum == GameManager.SoundEnum.IceCrack)
        {
            audioSourceIceCrack.clip = iceCrack;
            audioSourceIceCrack.volume = 0.5f;
            audioSourceIceCrack.Play();
        }
        if(soundEnum == GameManager.SoundEnum.Winning)
        {
            audioSourceWinning.clip = winning;
            audioSourceWinning.Play();
        }
        if(soundEnum == GameManager.SoundEnum.Coin)
        {
            audioSourceCoin.clip = coin;
            audioSourceCoin.volume = 0.6f;
            audioSourceCoin.Play();
        }
        if(soundEnum == GameManager.SoundEnum.Death)
        {
            audioSourceDeath.clip = death;
            audioSourceDeath.Play();    
        }
        if(soundEnum == GameManager.SoundEnum.ActivateGoal)
        {
            audioSourceActivateGoal.clip = activateGoal;
            audioSourceActivateGoal.volume = 0.8f;
            audioSourceActivateGoal.Play();
        } 
    }
}
