using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotCap : MonoBehaviour
{
    [SerializeField] private GameObject capOnPot;
    [SerializeField] private Transform originalPosition;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pot"))
        {
            capOnPot.SetActive(true);
            gameObject.SetActive(false);

            transform.position = originalPosition.position;
            transform.rotation = Quaternion.identity;

            DragItem dragItem = GetComponent<DragItem>();
            dragItem.isDragging = false;

            EventManager.GamePaused();
        }
    }

    public void ReturnBack()
    {
        capOnPot.SetActive(false);
        gameObject.SetActive(true);
        
    }

}
