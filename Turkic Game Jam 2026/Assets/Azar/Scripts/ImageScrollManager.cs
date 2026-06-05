using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScrollManager : MonoBehaviour
{
    [SerializeField] private GameObject foodItemPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform destroyTransform;
    [SerializeField] private List<Sprite> foodSprites;

    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float spawnInterval = 1.5f;

    private readonly List<GameObject> activeItems = new();

    private float spawnTimer;

    private void Update()
    {
        SpawnTimer();
        MoveItems();
        DestroyPassedItems();
    }

    private void SpawnTimer()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (foodItemPrefab == null)
        {
            Debug.LogWarning("Food item prefab is not assigned.");
            return;
        }

        if (spawnTransform == null)
        {
            Debug.LogWarning("Spawn transform is not assigned.");
            return;
        }

        Vector3 spawnPosition = spawnTransform.position;

        GameObject item = Instantiate(foodItemPrefab, spawnPosition, Quaternion.identity);
        AssignRandomSprite(item);
        activeItems.Add(item);
    }

    private void AssignRandomSprite(GameObject item)
    {
        if (foodSprites == null || foodSprites.Count == 0)
        {
            Debug.LogWarning("Food sprites list is empty.");
            return;
        }

        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("Spawned food item does not have a SpriteRenderer.");
            return;
        }
        Sprite randomSprite = foodSprites[Random.Range(0, foodSprites.Count)];
        spriteRenderer.sprite = randomSprite;
    }

    private void MoveItems()
    {
        for (int i = 0; i < activeItems.Count; i++)
        {
            if (activeItems[i] == null)
                continue;
            activeItems[i].transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }
    }

    private void DestroyPassedItems()
    {
        if (destroyTransform == null)
        {
            Debug.LogWarning("Destroy transform is not assigned.");
            return;
        }

        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            GameObject item = activeItems[i];

            if (item == null)
            {
                activeItems.RemoveAt(i);
                continue;
            }

            if (item.transform.position.x <= destroyTransform.position.x)
            {
                activeItems.RemoveAt(i);
                Destroy(item);
            }
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = Mathf.Max(0f, newSpeed);
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = Mathf.Max(0.01f, newInterval);
    }
}
