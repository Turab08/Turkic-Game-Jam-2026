    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DragItem : MonoBehaviour
    {
        private Camera mainCamera;
        private Rigidbody2D rb;
        private Vector2 velocity = Vector2.zero;

        public float followTime = 0.1f;
        private bool isDragging = false;
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
                    hit.transform.localScale *= 1.1f;
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
                rb.MovePosition(nextPos);
            }
        }
    }