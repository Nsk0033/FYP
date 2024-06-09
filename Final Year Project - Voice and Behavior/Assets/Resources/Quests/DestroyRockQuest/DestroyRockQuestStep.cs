using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRockQuestStep : QuestStep
{
	[SerializeField] private GameObject FineRock;
	
    private void Start()
    {
		FineRock = GameObject.Find("FineRock-Quest");
        if (FineRock != null)
        {
            string status = "Destroy The Rock With Axe";
            ChangeState("", status);
        }
        else
        {
            Debug.LogError("FineRock-Quest not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!FineRock.activeSelf)
		{
			string status = "The Giant Rock Has Been Destroyed";
            ChangeState("", status);
            FinishQuestStep();
		}
    }
	
	protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
