using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CursorLockManager : MonoBehaviour
{
    [SerializeField] private GameObject QuestLogMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject EndingMenu;
    [SerializeField] private GameObject MapMenu;
    [SerializeField] private GameObject MainCharacter; // Reference to the GameObject that contains the StarterAssetsInputs component

    private StarterAssetsInputs starterAssetsInputs;
	private PlayerInput playerInput;

    void Start()
    {
        // Find the StarterAssetsInputs component on the specified GameObject
        if (MainCharacter != null)
        {
			playerInput = MainCharacter.GetComponent<PlayerInput>();
            starterAssetsInputs = MainCharacter.GetComponent<StarterAssetsInputs>();
            if (starterAssetsInputs == null)
            {
                Debug.LogError("StarterAssetsInputs component not found on " + MainCharacter.name);
            }
        }
        else
        {
            Debug.LogError("MainCharacter is not assigned.");
        }
    }

    void Update()
    {
        if (starterAssetsInputs != null && playerInput != null)
        {
            bool isMenuActive = QuestLogMenu.activeSelf || PauseMenu.activeSelf || EndingMenu.activeSelf || MapMenu.activeSelf;

            starterAssetsInputs.ToggleMenuOnOff(isMenuActive);

            if (isMenuActive)
            {
                playerInput.enabled = false; // Disable PlayerInput when any menu is active
            }
            else
            {
                playerInput.enabled = true; // Enable PlayerInput when no menu is active
            }
        }
    }
}
