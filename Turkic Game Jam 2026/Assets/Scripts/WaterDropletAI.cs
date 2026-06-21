using System.Collections;
using UnityEngine;

public class WaterDropletAI : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Vector2 finalPos;
    private Vector2 currentVelocity = Vector2.zero;
    public float smoothTime;

    private SpriteRenderer spriteRenderer;
    private Material propertyMaterial;

    public ParticleSystem deathParticle;
    
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
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

    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        Vector2 newPos = Vector2.SmoothDamp(currentPos, targetPos, ref currentVelocity, smoothTime);

        transform.position = newPos;
    }

    public void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        ParticleSystem particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.waterSplash);
        EventManager.InteractableHovered(false, "Attack");
        Destroy(gameObject);
        yield return new WaitForSeconds(1);
        Destroy(particle.gameObject);
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
