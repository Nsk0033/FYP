using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill3OrthrosQuestStep : QuestStep
{
    [SerializeField] private GameObject SpawnObjectPrefab;
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private Transform SpawnLocation1;
    [SerializeField] private Transform SpawnLocation2;
    private GameObject spawnedObject;
    private GameObject spawnedObject1;
    private GameObject spawnedObject2;
    private int killStreak = 0;
    private bool isSpawnedObjectDead = false;
    private bool isSpawnedObject1Dead = false;
    private bool isSpawnedObject2Dead = false;

    private void Start()
    {
        // Spawn the SpawnObject
        spawnedObject = Instantiate(SpawnObjectPrefab, SpawnLocation.position, transform.rotation);
        spawnedObject1 = Instantiate(SpawnObjectPrefab, SpawnLocation1.position, transform.rotation);
        spawnedObject2 = Instantiate(SpawnObjectPrefab, SpawnLocation2.position, transform.rotation);

        UpdateStatusText();
    }

    void Update()
    {
        if (!isSpawnedObjectDead && !spawnedObject.activeSelf)
        {
            isSpawnedObjectDead = true;
            killStreak++;
            UpdateStatusText();
        }
        
        if (!isSpawnedObject1Dead && !spawnedObject1.activeSelf)
        {
            isSpawnedObject1Dead = true;
            killStreak++;
            UpdateStatusText();
        }
        
        if (!isSpawnedObject2Dead && !spawnedObject2.activeSelf)
        {
            isSpawnedObject2Dead = true;
            killStreak++;
            UpdateStatusText();
        }

        if (killStreak == 3)
        {
            FinishQuestStep();
            string status = "Report back to the little girl.";
            ChangeState("", status);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            FinishQuestStep();
        }
    }

    private void UpdateStatusText()
    {
        string status = "Kill " + killStreak + " / 3 of the Orthros outside the village.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
