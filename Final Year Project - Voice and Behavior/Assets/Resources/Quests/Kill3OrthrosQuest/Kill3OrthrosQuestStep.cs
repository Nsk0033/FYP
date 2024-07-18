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
	

    private void Start()
    {
        // Spawn the SpawnObject
        spawnedObject = Instantiate(SpawnObjectPrefab, SpawnLocation.position, transform.rotation);
        spawnedObject1 = Instantiate(SpawnObjectPrefab, SpawnLocation1.position, transform.rotation);
        spawnedObject2 = Instantiate(SpawnObjectPrefab, SpawnLocation2.position, transform.rotation);

        string status = "Kill " + killStreak + " / 3 of the Orthros";
        ChangeState("", status);
    }

    void Update()
    {
        if (!spawnedObject.activeSelf)
        {
			killStreak++;
            string status =  "Kill " + killStreak + " / 3 of the Orthros";
            ChangeState("", status);
            
        }
		if (!spawnedObject1.activeSelf)
        {
			killStreak++;
            string status =  "Kill " + killStreak + " / 3 of the Orthros";
            ChangeState("", status);
            
        }
		if (!spawnedObject2.activeSelf)
        {
			killStreak++;
            string status =  "Kill " + killStreak + " / 3 of the Orthros";
            ChangeState("", status);
            
        }
		if(killStreak == 3)
		{
			FinishQuestStep();
			string status =  "Report back to little girl";
			ChangeState("", status);
		}
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
