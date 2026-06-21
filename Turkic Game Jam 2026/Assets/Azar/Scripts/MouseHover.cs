using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour
{
    [SerializeField] private Texture2D interactableMouseTexture;
    [SerializeField] private Texture2D grabbedMouseTexture;
    [SerializeField] private Texture2D attackMouseTexture;

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

    private void Handle_OnInteractableHover(bool hoverState, string iconType)
    {
        Texture2D iconToApply = null;

        switch(iconType)
        {
            case "Grab":
                iconToApply = interactableMouseTexture;
                break;
            case "Attack":
                iconToApply = attackMouseTexture;
                break;
            default:
                iconToApply = null;
                break;
        }

        if(hoverState == false)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(iconToApply, Vector2.zero, CursorMode.Auto);
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
