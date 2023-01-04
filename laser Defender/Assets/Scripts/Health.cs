using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    [SerializeField] bool applyCameraShake;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer!=null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
            audioPlayer.PlayDamageClip();
        }
    }
    void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health<=0)
        {
            audioPlayer.PlayExplosionClip();
            Destroy(gameObject);
        }
    }
    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if(cameraShake!=null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
