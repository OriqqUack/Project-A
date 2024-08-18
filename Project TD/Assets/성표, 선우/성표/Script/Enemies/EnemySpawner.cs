using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    private int enemiesToSpawn;
    private int currentEnemyCount;
    private int enemiesSpawnedSoFar;
    private int enemyMaxConcurrentSpawnNumber;
    private TileEnemySpawnParameters tileEnemySpawnParameters;

    private int GetConcurrentEnemies()
    {
        return (Random.Range(tileEnemySpawnParameters.minConcurrentEnemies, tileEnemySpawnParameters.maxConcurrentEnemies));
    }
    /*
        private void SpawnEnemies()
        {
            StartCoroutine(SpawnEnemiesRoutine());
        }

        /// <summary>
        /// Spawn the enemies coroutine
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemiesRoutine()
        {
            Tile tile = GetComponent<Tile>();

            // GameObject를 추후 몬스터 디테일로 연결해야함**
            RandomSpawnableObject<GameObject> randomEnemyHelperClass = new RandomSpawnableObject<GameObject>(tile.spawnableObjectsByLevelList);

            if (tile.spawnPositionArray.Length > 0)
            {
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    while (currentEnemyCount >= enemyMaxConcurrentSpawnNumber)
                    {
                        yield return null;
                    }

                    Vector3 Position = tile.spawnPositionArray[Random.Range(0, tile.spawnPositionArray.Length)];

                    CreateEenmy(randomEnemyHelperClass.GetItem(), Position);

                    yield return new WaitForSeconds(GetEnemySpawnInterval());
                }
            }
        }

        private void CreateEenmy(EnemyDetailsSO enemyDetails, Vector3 position)
        {
            enemiesSpawnedSoFar++;

            currentEnemyCount++;

            Tile tile = GetComponent<Tile>();

            TileDetailSO tileLevel = tile.tileDetails;

            GameObject enemy = Instantiate(enemyDetails.enemyPrefab, position, Quaternion.identity, transform);

            enemy.GetComponent<Enemy>().EnemyInitialization(enemyDetails, enemiesSpawnedSoFar, tileLevel.mapLevel);

            enemy.GetComponent<DestroyedEvent>().OnDestroyed += Enemy_OnDestroyed;
        }

        private float GetEnemySpawnInterval()
        {
            return (Random.Range(tileEnemySpawnParameters.minSpawnInterval, tileEnemySpawnParameters.maxSpawnInterval));
        }

        private void Enemy_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
        {
            currentEnemyCount--;
        }
    */
}
