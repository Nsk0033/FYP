using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLavaOverflowQuestStep : QuestStep
{
    [SerializeField] private GameObject FreezableLava;
	[SerializeField] private bool matChanged;
	
    private SkillUnlockManager skillUnlockManager;

    private void Start()
    {
        skillUnlockManager = FindObjectOfType<SkillUnlockManager>();
        if (skillUnlockManager == null)
        {
            Debug.LogError("SkillUnlockManager not found in the scene.");
        }
        else
        {
            // Unlock skill2 at the start
            skillUnlockManager.UnlockSkill(2);
        }

        FreezableLava = GameObject.Find("mountain1-lava-Quest");
        if (FreezableLava != null)
        {
            matChanged = FreezableLava.GetComponent<ObjectFreezingScript>().isChanging;
            string status = "Cold Down Largest Volcano Lava With Ice Enchanted Axe.";
            ChangeState("", status);
        }
        else
        {
            string status = "Talk to the Blizzard God Acolyte.";
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
