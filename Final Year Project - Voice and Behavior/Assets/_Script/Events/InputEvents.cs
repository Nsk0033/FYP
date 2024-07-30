using UnityEngine;
using System;

public class InputEvents
{
    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir) 
    {
        if (onMovePressed != null) 
        {
            onMovePressed(moveDir);
        }
    }

    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null) 
        {
            onSubmitPressed();
        }
    }

    public event Action onQuestLogTogglePressed;
    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null) 
        {
            onQuestLogTogglePressed();
        }
    }
	
	public event Action onPausePressed;
    public void PausePressed()
    {
        if (onPausePressed != null) 
        {
            onPausePressed();
        }
    }
	
	public event Action onMapPressed;
    public void MapPressed()
    {
        if (onMapPressed != null) 
        {
            onMapPressed();
        }
    }
}
