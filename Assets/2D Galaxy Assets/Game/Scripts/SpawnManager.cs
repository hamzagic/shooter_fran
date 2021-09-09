using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private GameObject _asteroid;

    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
    }

    public void StartSpawnCoroutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while(true)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while(true)
        {
            int powerItem = Random.Range(0, 3);
            Instantiate(powerups[powerItem], new Vector3(Random.Range(-9.0f, 9.0f), 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator AsteroidSpawnRoutine()
    {
        while (true)
        {
            Instantiate(_asteroid, new Vector3(Random.Range(-9.0f, 9.0f), 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
