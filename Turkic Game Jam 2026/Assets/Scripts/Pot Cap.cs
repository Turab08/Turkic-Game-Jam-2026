using UnityEngine;

public class PotCap : MonoBehaviour
{
    [SerializeField] private GameObject capOnPot;
    [SerializeField] private Transform originalPosition;

    private DragItem dragItem;
    private SpriteRenderer spriteRenderer;
    private Material propertyMaterial;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        propertyMaterial = spriteRenderer.material;

        // outline is off at the start
        propertyMaterial.SetFloat("_IsHovered", 0f);
        //propertyMaterial.SetFloat("_OutlineThickness", 4f);
        dragItem = GetComponent<DragItem>();
    }

    void Update()
    {
        if (!dragItem.isDragging)
        {
            ReturnBack();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pot"))
        {
            capOnPot.SetActive(true);
            gameObject.SetActive(false);
            dragItem.isDragging = false;
            EventManager.DraggingProcess(false, collision.gameObject);
            EventManager.GamePaused();
        }
    }

    public void ReturnBack()
    {
        capOnPot.SetActive(false);
        gameObject.SetActive(true);

        transform.position = originalPosition.position;
        transform.rotation = Quaternion.identity;

        dragItem.isDragging = false;
    }

    private void OnMouseEnter()
    {
        EventManager.InteractableHovered(true);
        propertyMaterial.SetFloat("_IsHovered", 1f);
    }

    private void OnMouseExit()
    {
        EventManager.InteractableHovered(false);
        propertyMaterial.SetFloat("_IsHovered", 0f);
    }
}
