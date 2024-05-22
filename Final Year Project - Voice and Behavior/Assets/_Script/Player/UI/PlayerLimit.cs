using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimit : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxLimit = 300;
    [SerializeField] private int currentLimit;
    //[SerializeField] private gameObject healthBar;
	[SerializeField] private DuloGames.UI.UIProgressBar limitProgressBar;
	//private UIProgressBar uIProgressBar
	private float LimitPercentage = 0f;
	
	private void Start()
	{
		currentLimit = 0;
		
		limitProgressBar.fillAmount = LimitPercentage;
	}
	
	void Update()
    {
		LimitPercentage = (float)currentLimit / maxLimit;
		limitProgressBar.fillAmount = LimitPercentage;
        // Example: increase the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.UpArrow))
        {
			if(currentLimit < maxLimit)
				currentLimit++;
			else
				currentLimit = maxLimit;
        }

        // Example: decrease the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.DownArrow))
        {
			if(currentLimit > 0f)
				currentLimit--;
			else
				Debug.Log("Player Used Ulti!");
        }
		if(Input.GetKey(KeyCode.I))
		{
			if(currentLimit == maxLimit)
				currentLimit = 0;
		}
    }
}
