using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill1CerberusQuestStep : QuestStep
{
    [SerializeField] private GameObject SpawnObjectPrefab;
	[SerializeField] private Transform SpawnLocation;
    private GameObject spawnedObject;
	private SkillUnlockManager skillUnlockManager;

    private void Start()
    {
        // Spawn the SpawnObject
        spawnedObject = Instantiate(SpawnObjectPrefab, SpawnLocation.position, transform.rotation);

        // Find SkillUnlockManager in the scene
        skillUnlockManager = FindObjectOfType<SkillUnlockManager>();
        if (skillUnlockManager == null)
        {
            Debug.LogError("SkillUnlockManager not found in the scene.");
        }
		else
        {
            // Unlock skill2 at the start
            skillUnlockManager.UnlockSkill(3);
        }

        string status = "Kill the Cerberus";
        ChangeState("", status);
    }

    void Update()
    {
        if (!spawnedObject.activeSelf)
        {
            string status = "Report to the Thunder God's Oracle";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
