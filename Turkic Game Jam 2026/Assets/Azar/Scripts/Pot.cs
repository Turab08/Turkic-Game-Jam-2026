using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private int scoreIncrease = 20;
    [SerializeField] private int penaltyScore = -10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            if(other.gameObject.GetComponent<FoodItem>().GetFoodData() == GameManager.instance.GetCurrentFoodToBeCooked())
            {
                EventManager.FoodCooked();
                EventManager.ScoreChanged(scoreIncrease);
            }
            else
            {
                EventManager.ScoreChanged(penaltyScore);
            }
            Destroy(other.gameObject);
        }
    }
}
