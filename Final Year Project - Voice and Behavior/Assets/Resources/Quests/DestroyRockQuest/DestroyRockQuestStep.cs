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
            string status = "Destroy The Rock With Axe.";
            ChangeState("", status);
        }
        else
        {
            string status = "Talk to the Aiden who get stucked by giant rock.";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!FineRock.activeSelf)
		{
			string status = "Talk to the Aiden who get stucked by giant rock.";
            ChangeState("", status);
            FinishQuestStep();
		}
    }
	
	protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
