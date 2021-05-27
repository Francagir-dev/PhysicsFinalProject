using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private int numberOfEnemiesToSPawn;


    [SerializeField] private Vector3 positionOfEnemy;
    [SerializeField] private float timeToSpawn;

    private float xRandom, yRandom, zRandom;

    private GameObject VikingSpawned;

    private List<GameObject> vikings = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnVikingsEnemies", 0f, 20);
    }


   public void SpawnVikingsEnemies()
    {
        if (_enemyPrefab != null)
        {
            for (int i = 0; i < numberOfEnemiesToSPawn; i++)
            {
                positionOfEnemy = new Vector3(Random.Range(-11f, 15f ),5f,Random.Range(100f,50f)); 
                VikingSpawned =  Instantiate(_enemyPrefab, positionOfEnemy,Quaternion.Euler(0,180,0));
                vikings.Add(VikingSpawned);
            }
        }

    }
}