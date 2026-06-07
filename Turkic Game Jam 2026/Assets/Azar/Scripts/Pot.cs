using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private int scoreIncrease = 20;
    [SerializeField] private int penaltyScore = -10;
    [SerializeField] private ParticleSystem splashParticle;
    [SerializeField] private Transform particleSpawnPos;

    [SerializeField] private Animator scoreAnimator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Interactable"))
        {
            ParticleSystem particle = Instantiate(splashParticle, particleSpawnPos.position, Quaternion.identity);
            Destroy(particle.gameObject, 2);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.ingredientAdded);
            var ingredients = GameManager.instance.GetIngredients();

            bool ingredientMatched = false;
            for(int i = 0; i < ingredients.Count; i++)
            {
                var item = ingredients[i];
                if(item.Value == false && item.Key == other.gameObject.GetComponent<FoodItem>().GetFoodData())
                {
                    ingredients[i] = new KeyValuePair<FoodData, bool>(item.Key, true);
                    ingredientMatched = true;
                    EventManager.IngredientMatched(i, false);
                    break;
                }
            }

            if(!ingredientMatched)
            {
                EventManager.ScoreChanged(penaltyScore);
                EventManager.FoodCooked();
                Destroy(other.gameObject);
                EventManager.IngredientMatched(0, true);

                StartCoroutine(PlayScoreAnimation("HasDecreased"));
                return;
            }

            bool allIngredientsAdded = true;
            foreach (var item in ingredients)
            {
                if(item.Value == false)
                {
                    allIngredientsAdded = false;
                    break;
                }
            }

            if(allIngredientsAdded)
            {
                EventManager.FoodCooked();
                EventManager.ScoreChanged(scoreIncrease);
                EventManager.IngredientMatched(0, true);

                StartCoroutine(PlayScoreAnimation("HasIncreased"));
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayScoreAnimation(string name)
    {
        scoreAnimator.SetBool(name, true);
        yield return new WaitForSeconds(0.4f);
        scoreAnimator.SetBool(name, false);
    }
}
