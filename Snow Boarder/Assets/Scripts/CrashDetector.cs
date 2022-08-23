using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float reloadDelay;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    bool hasCrashed =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            FindObjectOfType<PlayerController>().DisableControl();
            Invoke("ReloadScene", reloadDelay);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
