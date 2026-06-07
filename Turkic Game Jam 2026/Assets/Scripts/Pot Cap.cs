using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotCap : MonoBehaviour
{
    [SerializeField] private GameObject capOnPot;
    [SerializeField] private Transform originalPosition;

    private DragItem dragItem;

    void Start()
    {
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

}
