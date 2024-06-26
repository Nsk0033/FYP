using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect4EmeraldQuestStep : QuestStep
{
    [SerializeField] private GameObject SpawnObjectPrefab;
    [SerializeField] private Transform[] SpawnLocations;
    
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int emeraldCollected = 0;
    private int emeraldToComplete = 4;

    private void Start()
    {
        // Spawn the objects at the specified locations
        foreach (Transform location in SpawnLocations)
        {
            GameObject spawnedObject = Instantiate(SpawnObjectPrefab, location.position, location.rotation);
            spawnedObjects.Add(spawnedObject);
        }
		UpdateState();
    }

    private void Update()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
		{
			if (!spawnedObjects[i].activeSelf)
			{
				emeraldCollected++;
				spawnedObjects.RemoveAt(i);
				UpdateState();

				if (emeraldCollected >= emeraldToComplete)
				{
					string status = "Talk to the Blacksmith in Forge.";
					ChangeState("", status);
					FinishQuestStep();
				}
			}
		}
    }

    private void UpdateState()
    {
        string state = emeraldCollected.ToString();
        string status = "Collected " + emeraldCollected + " / " + emeraldToComplete + " emeralds.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.emeraldCollected = System.Int32.Parse(state);
        UpdateState();
    }
	
	
}
