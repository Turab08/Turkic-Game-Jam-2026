using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    private FoodData foodObject;
    private void OnEnable()
    {
        EventManager.OnDraggingProcess += Handle_OnDraggingProcess;
    }

    private void OnDisable()
    {
        EventManager.OnDraggingProcess -= Handle_OnDraggingProcess;
    }

    private void Handle_OnDraggingProcess(bool dragState, GameObject draggedObject)
    {
        if(draggedObject != this.gameObject)
            return;

        if(dragState)
        {
            GameManager.instance.RemoveItem(this.gameObject);
            return;
        }
        else
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            BoxCollider2D bc = GetComponent<BoxCollider2D>();

            rb.gravityScale = 1.0f;
            bc.enabled = false;
        }

        Destroy(this.gameObject, 5f);
    }

    public void Initialize(FoodData obj)
    {
        foodObject = obj;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("Spawned food item does not have a SpriteRenderer.");
            return;
        }
        spriteRenderer.sprite = foodObject.foodImage;
    }

    public FoodData GetFoodData()
    {
        return foodObject;
    }
}
