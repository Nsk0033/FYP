using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill2GoulUndertakerQuestStep : QuestStep
{
    [SerializeField] private GameObject SpawnObjectPrefab;
    [SerializeField] private GameObject SpawnObjectPrefab1;
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private Transform SpawnLocation1;
    [SerializeField] private Transform SpawnLocation2;
    [SerializeField] private Transform SpawnLocation3;
    private GameObject spawnedObject;
    private GameObject spawnedObject1;
    private GameObject spawnedObject2;
    private GameObject spawnedObject3;
    private int killStreak = 0;
    private int killStreak1 = 0;
    private bool isSpawnedObjectDead = false;
    private bool isSpawnedObject1Dead = false;
    private bool isSpawnedObject2Dead = false;
    private bool isSpawnedObject3Dead = false;

    private void Start()
    {
        // Spawn the SpawnObject
        spawnedObject = Instantiate(SpawnObjectPrefab, SpawnLocation.position, transform.rotation);
        spawnedObject1 = Instantiate(SpawnObjectPrefab, SpawnLocation1.position, transform.rotation);
        spawnedObject2 = Instantiate(SpawnObjectPrefab1, SpawnLocation2.position, transform.rotation);
        spawnedObject3 = Instantiate(SpawnObjectPrefab1, SpawnLocation3.position, transform.rotation);

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
            killStreak1++;
            UpdateStatusText();
        }

        if (!isSpawnedObject3Dead && !spawnedObject3.activeSelf)
        {
            isSpawnedObject3Dead = true;
            killStreak1++;
            UpdateStatusText();
        }

        if (killStreak == 2 && killStreak1 == 2)
        {
            FinishQuestStep();
            string status = "Report back to the guard";
            ChangeState("", status);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            FinishQuestStep();
        }
    }

    private void UpdateStatusText()
    {
        string status = "Kill " + killStreak + " / 2 of the Goul, " + killStreak1 + " / 2 of the Undertaker behind the temple.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
