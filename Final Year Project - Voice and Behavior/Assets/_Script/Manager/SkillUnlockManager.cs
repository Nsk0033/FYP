using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlockManager : MonoBehaviour
{
	[SerializeField] private GameObject Skill1Slot; // whole panel
	[SerializeField] private GameObject Skill2Slot; // whole panel
	[SerializeField] private GameObject Skill3Slot; // whole panel
	
    public bool isSkill1Unlocked = false;
    public bool isSkill2Unlocked = false;
    public bool isSkill3Unlocked = false;
	
	void Start()
	{
		Skill1Slot.SetActive(false);
		Skill2Slot.SetActive(false);
		Skill3Slot.SetActive(false);
	}
	
    // This method will be called to unlock a specific skill
    public void UnlockSkill(int skillId)
    {
        switch (skillId)
        {
            case 1:
                isSkill1Unlocked = true;
				Skill1Slot.SetActive(true);
                break;
            case 2:
                isSkill2Unlocked = true;
				Skill2Slot.SetActive(true);
                break;
            case 3:
                isSkill3Unlocked = true;
				Skill3Slot.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid skill ID");
                break;
        }
    }
	
	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnlockAllSkills();
        }
    }

    private void UnlockAllSkills()
    {
        UnlockSkill(1);
        UnlockSkill(2);
        UnlockSkill(3);
    }
}
