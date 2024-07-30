using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public int originalGroupSize;
        [HideInInspector]
        public int currentGroupSize;
        public List<GameObject> spawnedEnemies;
        [HideInInspector]
        public bool respawnScheduled = false;
    }

    [SerializeField] private List<EnemyGroup> enemyGroups;
    [SerializeField] private float respawnDelay = 300f; // 5 minutes
    [SerializeField] private BoxCollider spawnArea;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        foreach (var group in enemyGroups)
        {
            group.spawnedEnemies.RemoveAll(enemy => enemy == null);
            group.currentGroupSize = group.spawnedEnemies.Count;

            if (group.currentGroupSize < group.originalGroupSize && !group.respawnScheduled)
            {
                group.respawnScheduled = true;
                StartCoroutine(RespawnGroup(group));
            }
        }
    }

    private void SpawnEnemies()
    {
        foreach (var group in enemyGroups)
        {
            group.spawnedEnemies = new List<GameObject>();
            for (int i = 0; i < group.originalGroupSize; i++)
            {
                Vector3 spawnPosition = GetRandomPositionWithinBounds();
                GameObject enemy = Instantiate(group.enemyPrefab, spawnPosition, Quaternion.identity);
                group.spawnedEnemies.Add(enemy);
            }
            group.currentGroupSize = group.spawnedEnemies.Count;
        }
    }

    private IEnumerator RespawnGroup(EnemyGroup group)
    {
        yield return new WaitForSeconds(respawnDelay);

        int neededEnemies = group.originalGroupSize - group.currentGroupSize;
        for (int i = 0; i < neededEnemies; i++)
        {
            Vector3 spawnPosition = GetRandomPositionWithinBounds();
            GameObject enemy = Instantiate(group.enemyPrefab, spawnPosition, Quaternion.identity);
            group.spawnedEnemies.Add(enemy);
        }
        group.currentGroupSize = group.spawnedEnemies.Count;
        group.respawnScheduled = false;
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
