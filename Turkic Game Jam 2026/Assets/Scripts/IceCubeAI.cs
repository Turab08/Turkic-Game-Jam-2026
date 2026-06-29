using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeAI : Enemy
{
    Rigidbody2D rb;
    [SerializeField] private GameObject waterDroplet;

    [SerializeField] private Transform waterDropletSpawnPos1;
    [SerializeField] private Transform waterDropletSpawnPos2;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        Vector2 newPos = Vector2.SmoothDamp(currentPos, targetPos, ref currentVelocity, smoothTime);

        transform.position = newPos;
    }

    public void Die()
    {
        Death();
    }

    private void Death()
    {
        Instantiate(waterDroplet, waterDropletSpawnPos1.position, Quaternion.identity);
        Instantiate(waterDroplet, waterDropletSpawnPos2.position, Quaternion.identity);

        ParticleSystem particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.iceCrack);
        EventManager.InteractableHovered(false, "Attack");
        Destroy(particle.gameObject, 1);
        Destroy(gameObject);
    }
}
