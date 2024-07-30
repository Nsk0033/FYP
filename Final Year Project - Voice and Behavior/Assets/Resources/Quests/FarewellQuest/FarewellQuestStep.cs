using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarewellQuestStep : QuestStep
{
    [SerializeField] private GameObject portal;

    private void Start()
    {
		GameObject portalFinal = FindInActiveObjectByName("Portal-Final");
		if (portalFinal != null)
		{
			portalFinal.SetActive(true);
		}
		else
		{
			Debug.LogError("GameObject 'Portal-Final' not found in the hierarchy.");
		}
		
        string status = "Go through the portal.";
        ChangeState("", status);

        
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
			string status = "Talk to Fire God.";
			ChangeState("", status);
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // No state is needed for this quest step
    }
	
	private GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None && obj.name == name)
            {
                return obj.gameObject;
            }
        }
        return null;
    }
}
