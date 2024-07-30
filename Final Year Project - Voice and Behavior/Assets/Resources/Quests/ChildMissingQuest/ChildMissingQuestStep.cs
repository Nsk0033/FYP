using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMissingQuestStep : QuestStep
{
    [SerializeField] private GameObject content;
	
    private void Start()
    {
        string status = "Search around the area.";
        ChangeState("", status);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            //string status = "Interact with Fire God Statue.";
            //ChangeState("", status);
			content.SetActive(false);
            FinishQuestStep();
			//GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
