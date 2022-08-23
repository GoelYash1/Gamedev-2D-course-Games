using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            snowEffect.Play();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        snowEffect.Stop();
    }
}
