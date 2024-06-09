using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CursorLockManager : MonoBehaviour
{
	[SerializeField] private GameObject QuestLogUI;
	
	private StarterAssetsInputs starterAssetsInputs;
	
    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestLogUI.activeSelf)
			starterAssetsInputs.ToggleMenuOnOff(true);
		else
			starterAssetsInputs.ToggleMenuOnOff(false);
    }
}
