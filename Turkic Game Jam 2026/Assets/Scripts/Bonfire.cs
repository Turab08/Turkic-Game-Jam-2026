using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public float maxSize;
    public float minSize;

    private float currentSize;

    public float shringSpeed;

    public Transform flames;

    void Start()
    {
        currentSize = maxSize;
    }

    void Update() {
        currentSize -= shringSpeed * Time.deltaTime;
        currentSize = Mathf.Clamp(currentSize, minSize, maxSize);
        flames.localScale = new Vector3(flames.localScale.x, currentSize, flames.localScale.z); 
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Wood"))
        {
            currentSize += 0.5f;
            Destroy(other.gameObject);
        }
    }
}
