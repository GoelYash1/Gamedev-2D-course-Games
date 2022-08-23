using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float audioVolume = 0.2f;
    float playDelay = 0.3f;
    GameManager gameManager;
    AudioSource audioSource;
    [SerializeField] AudioClip quizWaitingAudioClip;
    [SerializeField] List<AudioClip> quizAudioClips;
    [SerializeField] AudioClip endAudioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource.clip = quizWaitingAudioClip;
        audioSource.volume = audioVolume + 0.2f;
        audioSource.Play();
    }
    public void PlayAudio()
    {
        audioSource.volume = audioVolume;
        if(gameManager.IsCategorySelected())
        {
            audioSource.Stop();
            audioSource.clip = quizAudioClips[gameManager.GetSelectedCategoryIndex()];
            audioSource.PlayDelayed(playDelay);
        }
        if(gameManager.GameComplete())
        {
            audioSource.Stop();
            audioSource.PlayOneShot(endAudioClip);
        }
    }
}
