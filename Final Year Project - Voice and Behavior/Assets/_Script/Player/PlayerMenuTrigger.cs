using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerMenuTrigger : MonoBehaviour
{
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	
    private void Awake()
	{
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
	}

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.interact)
		{
			GameEventsManager.instance.inputEvents.SubmitPressed();
		}
    
        if (starterAssetsInputs.questLogToggle)
        {
            GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
			starterAssetsInputs.questLogToggle = false;
			//starterAssetsInputs.ToggleMenu();
        }
		
		if(starterAssetsInputs.pause)
		{
			GameEventsManager.instance.inputEvents.PausePressed();
			starterAssetsInputs.pause = false;
		}
		
		if(starterAssetsInputs.map)
		{
			GameEventsManager.instance.inputEvents.MapPressed();
			starterAssetsInputs.map = false;
		}
    }
}
