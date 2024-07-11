using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLavaOverflowQuestStep : QuestStep
{
    [SerializeField] private GameObject FreezableLava;
	[SerializeField] private bool matChanged;
	
    private void Start()
    {
		FreezableLava = GameObject.Find("mountain1-lava-Quest");
        if (FreezableLava != null)
        {
			matChanged = FreezableLava.GetComponent<ObjectFreezingScript>().isChanging;
            string status = "Cold Down Largest Volcano Lava With Ice Enchanted Axe.";
            ChangeState("", status);
        }
        else
        {
            string status = "Talk to the Blizzard God Acolte.";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    // Update is called once per frame
    void Update()
    {
		matChanged = FreezableLava.GetComponent<ObjectFreezingScript>().isChanging;
		
        if(matChanged)
		{
			string status = "Talk to the Blizzard God Acolte.";
            ChangeState("", status);
            FinishQuestStep();
		}
    }
	
	protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
