using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private int scoreIncrease = 20;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            Destroy(other.gameObject);
            EventManager.FoodCooked();
            EventManager.ScoreChanged(scoreIncrease);
        }
    }
}
