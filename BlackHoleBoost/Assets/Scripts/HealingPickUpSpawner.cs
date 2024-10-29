using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> healingPickUpModels;
    [SerializeField] private float spawnRate;

    private void Start()
    {
        InvokeRepeating("SpawnHealingPickUp", 1, spawnRate);
    }
    private void SpawnHealingPickUp()
    {
        int modelIndex = Random.Range(0, healingPickUpModels.Count);
        float posX = Random.Range(-GameManager.Instance.RightBoundary, GameManager.Instance.RightBoundary);
        float posY = Random.Range(-GameManager.Instance.TopBoundary, GameManager.Instance.TopBoundary);
        Vector3 pos = new Vector3(posX, posY, 0);
        GameObject go = Instantiate(healingPickUpModels[modelIndex],pos,Quaternion.identity,transform);
    }
}
