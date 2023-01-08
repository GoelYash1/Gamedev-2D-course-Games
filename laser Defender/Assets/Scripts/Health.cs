using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    [SerializeField] bool isPlayer;
    [SerializeField] bool applyCameraShake;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
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
            Die();
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
    void Die()
    {
        audioPlayer.PlayExplosionClip();
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        Destroy(gameObject);
    }
    public int GetHealth()
    {
        return health;
    }
}
