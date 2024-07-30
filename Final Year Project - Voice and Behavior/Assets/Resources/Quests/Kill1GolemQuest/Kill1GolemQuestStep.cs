using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill1GolemQuestStep : QuestStep
{
    [SerializeField] private GameObject SpawnObjectPrefab;
	[SerializeField] private Transform SpawnLocation;
    private GameObject spawnedObject;

    private void Start()
    {
        // Spawn the SpawnObject
        spawnedObject = Instantiate(SpawnObjectPrefab, SpawnLocation.position, transform.rotation);

        string status = "Kill the Golem";
        ChangeState("", status);
    }

    void Update()
    {
        if (!spawnedObject.activeSelf)
        {
            string status = "Report to the Guard";
            ChangeState("", status);
            FinishQuestStep();
        }
		if (Input.GetKeyDown(KeyCode.K))
		{
			FinishQuestStep();
		}
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
