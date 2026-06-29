using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndQuitHighlight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material propertyMaterial;

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
