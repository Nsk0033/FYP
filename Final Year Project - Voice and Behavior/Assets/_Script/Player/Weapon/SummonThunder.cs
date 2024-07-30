using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonThunder : MonoBehaviour
{
    [SerializeField] private GameObject thunderPrefab;
    private List<Transform> enemiesInRange = new List<Transform>();

    private void Start()
    {
        Invoke("StartThunderSpawning", 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Add enemy to the list if not already added
            if (!enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Remove enemy from the list
            enemiesInRange.Remove(other.transform);
        }
    }

    private void SpawnThunder()
    {
        if (enemiesInRange.Count == 0)
        {
            // No enemies in range
            return;
        }

        int randomIndex = Random.Range(0, enemiesInRange.Count);
        Vector3 randomLocation = enemiesInRange[randomIndex].position;

        // Spawn thunder at the random location
        Instantiate(thunderPrefab, randomLocation, Quaternion.identity);
    }

    // Call this method to start the thunder spawning process
    public void StartThunderSpawning()
    {
        StartCoroutine(ThunderSpawningRoutine());
    }

    private IEnumerator ThunderSpawningRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f); // Delay between thunder spawns
            SpawnThunder();
        }
    }
}
