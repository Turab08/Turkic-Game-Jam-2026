using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public float maxSize;
    public float minSize;

    public float currentSize;

    public float shringSpeed;

    public Transform flames;

    public ParticleSystem damageParticle;

    void Start()
    {
        currentSize = maxSize;
    }

    void Update() {
        currentSize -= shringSpeed * Time.deltaTime;
        currentSize = Mathf.Clamp(currentSize, minSize, maxSize);
        flames.localScale = new Vector3(flames.localScale.x, currentSize, flames.localScale.z); 
        if(currentSize <= minSize)
        {
            EventManager.GameFinished();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Wood"))
        {
            currentSize += 0.5f;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Water"))
        {
            currentSize -= 0.2f;
            StartCoroutine(PlayParticle());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayParticle()
    {
        ParticleSystem particle = Instantiate(damageParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(particle);
    }
}
