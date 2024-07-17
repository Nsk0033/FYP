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
            // Add enemy to the list
            enemiesInRange.Add(other.transform);
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

    public void SpawnThunder()
    {
        if (enemiesInRange.Count == 0)
        {
            // No enemies in range
            return;
        }

        HashSet<int> selectedIndices = new HashSet<int>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex;

            // Ensure unique random index
            do
            {
                randomIndex = Random.Range(0, enemiesInRange.Count);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);

            Vector3 randomLocation = enemiesInRange[randomIndex].position;

            // Spawn thunder at the random location
            Instantiate(thunderPrefab, randomLocation, Quaternion.identity);
        }
    }

    // Call this method to start the thunder spawning process
    public void StartThunderSpawning()
    {
        StartCoroutine(ThunderSpawningRoutine());
    }

    private IEnumerator ThunderSpawningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Delay between thunder spawns
            SpawnThunder();
        }
    }
}
