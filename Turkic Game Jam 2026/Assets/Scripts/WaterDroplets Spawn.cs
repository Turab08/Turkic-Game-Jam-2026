using UnityEngine;

public class WaterDropletsSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject waterDropletObj;
    public Transform parentObj;

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
        SpawnDroplet();
    }

    void SpawnDroplet()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Instantiate(waterDropletObj, spawnPoints[rand].position, Quaternion.identity, parentObj);
    }
}
