using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonThunder : MonoBehaviour
{
    [SerializeField] private GameObject thunderPrefab;
    private List<Vector3> enemyLocations = new List<Vector3>();

	private void Start()
	{
		Invoke("StartThunderSpawning",0.2f);
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Store enemy location
            enemyLocations.Add(other.transform.position);
        }
    }
	
    public void SpawnThunder()
    {
        if (enemyLocations.Count == 0)
        {
            //Debug.LogWarning("No enemies in range to spawn thunder.");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            // Get a random enemy location
            int randomIndex = Random.Range(0, enemyLocations.Count);
            Vector3 randomLocation = enemyLocations[randomIndex];

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
        for (int i = 0; i < 3; i++)
        {
			yield return new WaitForSeconds(1f); // Delay between thunder spawns
            SpawnThunder();
        }
    }
}
