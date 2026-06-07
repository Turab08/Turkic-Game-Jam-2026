    using System.Collections;
    using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    public class DragItem : MonoBehaviour
    {
        private Camera mainCamera;
        private Rigidbody2D rb;
        private Vector2 velocity = Vector2.zero;

        public float followTime = 0.1f;
        public float rotationSpeed = 720f;
        public bool isDragging = false;
        private GameObject currentDraggedObject = null;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    currentDraggedObject = hit.collider.gameObject;
                    if(!hit.collider.gameObject.CompareTag("PlayButton") && !hit.collider.gameObject.CompareTag("QuitButton") && !hit.collider.gameObject.CompareTag("Cap"))
                    {
                        hit.transform.localScale *= 1.1f;
                    }
                    EventManager.DraggingProcess(true, currentDraggedObject);
                }
                if (hit.collider != null && hit.collider.CompareTag("Water"))
                {
                    WaterDropletAI waterDropletAI = hit.collider.gameObject.GetComponent<WaterDropletAI>();
                    waterDropletAI.Die();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    isDragging = false;
                    EventManager.DraggingProcess(false, currentDraggedObject);
                }
            }
            
        }

        void FixedUpdate()
        {
            if (isDragging)
            {
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 nextPos = Vector2.SmoothDamp(rb.position, mouseWorldPos, ref velocity, followTime);

                Vector2 moveDirection = nextPos - rb.position;
                rb.MovePosition(nextPos);

                RotateTowardsMovement(moveDirection);
            }
        }

        void RotateTowardsMovement(Vector2 moveDirection)
        {
            if (moveDirection.magnitude >= 0.001f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);        
            }
        }
    }