using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadDelay;
    [SerializeField] ParticleSystem finishEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Triumph!! Hooray");
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            FindObjectOfType<PlayerController>().DisableControl();
            Invoke("ReloadScene", reloadDelay);
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
