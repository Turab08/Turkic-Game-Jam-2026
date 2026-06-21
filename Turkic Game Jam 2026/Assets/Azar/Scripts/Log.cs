using UnityEngine;

public class Log : MonoBehaviour
{
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

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        propertyMaterial = spriteRenderer.material;

        // outline is off at the start
        propertyMaterial.SetFloat("_IsHovered", 0f);
        //propertyMaterial.SetFloat("_OutlineThickness", 4f);
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
