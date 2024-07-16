using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconManager : MonoBehaviour
{
    [SerializeField] private Image[] skillIconSlots; // UI Image components to display skill icons
    [SerializeField] private Sprite[,] skillIcons; // 2D array to hold skill icons

    private ThirdPersonShooterController thirdPersonShooterController;

    private void Awake()
    {
        // Get the reference to the ThirdPersonShooterController component
        thirdPersonShooterController = FindObjectOfType<ThirdPersonShooterController>();

        if (thirdPersonShooterController == null)
        {
            Debug.LogError("ThirdPersonShooterController not found in the scene.");
        }
    }

    private void Start()
    {
        // Initialize the skill icons array (replace with actual skill icon assignment)
        skillIcons = new Sprite[3, 5]; // 3 weapons, 5 skills each

        // Example initialization (replace with actual sprites)
        for (int weapon = 0; weapon < 3; weapon++)
        {
            for (int skill = 0; skill < 5; skill++)
            {
                skillIcons[weapon, skill] = Resources.Load<Sprite>($"Icons/Weapon{weapon}_Skill{skill}");
            }
        }

        // Update skill icons based on the current weapon index at the start
        UpdateSkillIcons(thirdPersonShooterController.CurrentWeaponIndex);
    }

    private void Update()
    {
        // Check if the weapon index has changed and update skill icons accordingly
        if (thirdPersonShooterController != null)
        {
            UpdateSkillIcons(thirdPersonShooterController.CurrentWeaponIndex);
        }
    }

    private void UpdateSkillIcons(int weaponIndex)
    {
        for (int i = 0; i < skillIconSlots.Length; i++)
        {
            if (i < skillIcons.GetLength(1))
            {
                skillIconSlots[i].sprite = skillIcons[weaponIndex - 1, i]; // Subtract 1 since weaponIndex is 1-based
            }
        }
    }
}