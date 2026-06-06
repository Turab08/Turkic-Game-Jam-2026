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

        Destroy(this.gameObject);
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
}
