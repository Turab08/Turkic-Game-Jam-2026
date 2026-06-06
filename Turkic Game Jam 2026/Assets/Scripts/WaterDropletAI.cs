using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterDropletAI : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Vector2 finalPos;
    private Vector2 currentVelocity = Vector2.zero;
    public float smoothTime;

    public ParticleSystem deathParticle;
    
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.Find("Bonfire").transform;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        Vector2 newPos = Vector2.SmoothDamp(currentPos, targetPos, ref currentVelocity, smoothTime);

        transform.position = newPos;
    }

    public void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        ParticleSystem particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        yield return null;
        Destroy(gameObject);
        yield return new WaitForSeconds(1);
        Destroy(particle);
    }
}
