using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected Transform target;
    protected Vector2 currentVelocity = Vector2.zero;
    [SerializeField] protected float smoothTime;

    protected SpriteRenderer spriteRenderer;
    protected Material propertyMaterial;

    [SerializeField] protected ParticleSystem deathParticle;


    private void Start()
    {
        target = GameObject.Find("Bonfire").transform;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        propertyMaterial = spriteRenderer.material;

        // outline is off at the start
        propertyMaterial.SetFloat("_IsHovered", 0f);
        propertyMaterial.SetFloat("_OutlineThickness", 8f);
    }


    private void OnMouseEnter()
    {
        EventManager.InteractableHovered(true, "Attack");
        propertyMaterial.SetFloat("_IsHovered", 1f);
        propertyMaterial.SetColor("_OutlineColor", Color.red);
    }

    private void OnMouseExit()
    {
        EventManager.InteractableHovered(false, "Attack");
        propertyMaterial.SetFloat("_IsHovered", 0f);
    }
}
