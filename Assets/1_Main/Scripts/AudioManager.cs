using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;    
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip menuBackground;
    public AudioClip gameplayBackground;
    public AudioClip buttonClick;
    public AudioClip playerSpam;
    public AudioClip playerKameha;
    public AudioClip playerDon;
    public AudioClip playerDragon;
    public AudioClip playerSpiritBoom;
    public AudioClip playerPowerUp;
    [SerializeField] private AudioClip[] playerHit;
    [SerializeField] private AudioClip[] enemyHit;
    public AudioClip playerDie;
    public AudioClip enemyDieDie;
    public AudioClip skillFly;
    public AudioClip vs;
    public AudioClip coinEarn;
    public AudioClip coinPay;
    public AudioClip cheer;
    public AudioClip item;
    public AudioClip cach;

    private Transform[] childs;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayMenuMusic()
    {
        musicSource.clip = menuBackground;
        musicSource.loop = true;
        musicSource.Play();
    } 

    public void PlayGameplayMusic()
    {
        musicSource.clip = gameplayBackground;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayPlayerHit()
    {
        SFXSource.PlayOneShot(playerHit[Random.Range(0, playerHit.Length)]);
    }

    public void PlayEnemyHit()
    {
        SFXSource.PlayOneShot(enemyHit[Random.Range(0, enemyHit.Length)]);
    }

    public void TurnOffMusic()
    {
        musicSource.mute = true;
    }

    public void TurnOffSFX()
    {
        SFXSource.mute = true;
    }



    public void TurnOnMusic()
    {
        musicSource.mute = false;
    }

    public void TurnOnSFX()
    {
        SFXSource.mute = false;
    }


}