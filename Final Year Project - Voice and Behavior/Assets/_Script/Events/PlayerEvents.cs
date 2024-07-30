using System;
using UnityEngine.Events;

public class PlayerEvents
{
	public UnityEvent<int> OnDealDamage = new UnityEvent<int>();
	
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null) 
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null) 
        {
            onEnablePlayerMovement();
        }
    }

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience) 
    {
        if (onExperienceGained != null) 
        {
            onExperienceGained(experience);
        }
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level) 
    {
        if (onPlayerLevelChange != null) 
        {
            onPlayerLevelChange(level);
        }
    }

    public event Action<int> onPlayerExperienceChange;
    public void PlayerExperienceChange(int experience) 
    {
        if (onPlayerExperienceChange != null) 
        {
            onPlayerExperienceChange(experience);
        }
    }
}
