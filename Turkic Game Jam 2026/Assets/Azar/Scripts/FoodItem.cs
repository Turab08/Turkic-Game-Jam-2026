using UnityEngine;

public class FoodItem : MonoBehaviour
{
    private FoodData foodObject;
    private SpriteRenderer spriteRenderer;
    private Material propertyMaterial;

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

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        propertyMaterial = spriteRenderer.material;

        // outline is off at the start
        propertyMaterial.SetFloat("_IsHovered", 0f);
    }

    public void Initialize(FoodData obj)
    {
        foodObject = obj;

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

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

    private void OnMouseEnter()
    {
        EventManager.InteractableHovered(true, "Grab");
        propertyMaterial.SetFloat("_IsHovered", 1f);
    }

    private void OnMouseExit()
    {
        EventManager.InteractableHovered(false, "Grab");
        propertyMaterial.SetFloat("_IsHovered", 0f);
    }
}
