using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScrollManager : MonoBehaviour
{
    [SerializeField] private GameObject foodItemPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform destroyTransform;
    [SerializeField] private List<FoodData> foodItems;
    [SerializeField] private Image currentFood;

    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float spawnInterval = 1.5f;

    private float spawnTimer;

    void OnEnable()
    {
        EventManager.OnFoodCooked += Handle_OnFoodCooked;
    }

    void OnDisable()
    {
        EventManager.OnFoodCooked -= Handle_OnFoodCooked;
    }

    void Start()
    {
        if(GameManager.instance.GetCurrentFoodToBeCooked() == null)
        {
            ChooseNextFood();
        }
    }

    private void ChooseNextFood()
    {
        FoodData chosenFood = foodItems[Random.Range(0, foodItems.Count)];
        GameManager.instance.SetCurrentFoodToBeCooked(chosenFood);
        currentFood.sprite = chosenFood.foodImage;
    }

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
        AssignRandomFood(item);
        GameManager.instance.AddItem(item);
    }

    private void AssignRandomFood(GameObject item)
    {
        if (foodItems == null || foodItems.Count == 0)
        {
            Debug.LogWarning("Food sprites list is empty.");
            return;
        }

        FoodItem foodItem = item.GetComponent<FoodItem>();
        if(foodItem == null)
        {
            Debug.LogWarning("No food item script attached");
        }

        FoodData randomFood = foodItems[Random.Range(0, foodItems.Count)];
        foodItem.Initialize(randomFood);
    }

    private void MoveItems()
    {
        var activeItems = GameManager.instance.GetActiveItems(); 
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

        var activeItems = GameManager.instance.GetActiveItems(); 
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

    private void Handle_OnFoodCooked()
    {
        ChooseNextFood();
    }
}
