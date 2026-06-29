using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] enemies;
    public Transform parentObj;

    [SerializeField] private Bonfire bonfire;

    void OnEnable()
    {
        EventManager.OnIngredientMatched += Handle_OnIngredientMatched;
    }

    void OnDisable()
    {
        EventManager.OnIngredientMatched -= Handle_OnIngredientMatched;
    }

    private void Handle_OnIngredientMatched(int index, bool reset)
    {
        if (bonfire.currentSize >= 0.6)
        {
            SpawnDroplet();
        }
        else
        {
            float rand = Random.Range(0f, 1f);
            Debug.Log(rand);
            if (rand < 0.3f)
            {
                SpawnIceCube();
            }
            else
            {
                SpawnDroplet();
            }
        }
    }

    void SpawnDroplet()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[0], spawnPoints[rand].position, Quaternion.identity, parentObj);
    }

    void SpawnIceCube()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[1], spawnPoints[rand].position, Quaternion.identity, parentObj);
    }
}
