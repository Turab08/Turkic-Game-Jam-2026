using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField] private Texture2D interactableMouseTexture;
    [SerializeField] private Texture2D grabbedMouseTexture;

    private void OnEnable()
    {
        EventManager.OnInteractableHovered += Handle_OnInteractableHover;
        EventManager.OnDraggingProcess += Handle_OnDraggingProcess;
    }

    private void OnDisable()
    {
        EventManager.OnInteractableHovered -= Handle_OnInteractableHover;
        EventManager.OnDraggingProcess -= Handle_OnDraggingProcess;

    }

    private void Handle_OnInteractableHover(bool hoverState)
    {
        if(hoverState == false)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(interactableMouseTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void Handle_OnDraggingProcess(bool dragState, GameObject draggedObject)
    {
        if(dragState == false)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(grabbedMouseTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}
