using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[Header("Health")]
    [SerializeField] private int maxHealth = 300;
    [SerializeField] private int currentHealth;
    //[SerializeField] private gameObject healthBar;
	[SerializeField] private DuloGames.UI.UIProgressBar healthProgressBar;
	//private UIProgressBar uIProgressBar
	private float HPPercentage = 0f;
	
	private void Start()
	{
		currentHealth = maxHealth;
		
		healthProgressBar.fillAmount = HPPercentage;
	}
	
	void Update()
    {
		HPPercentage = (float)currentHealth / maxHealth;
		healthProgressBar.fillAmount = HPPercentage;
        // Example: increase the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.UpArrow))
        {
			if(currentHealth < maxHealth)
				currentHealth++;
			else
				currentHealth = maxHealth;
        }

        // Example: decrease the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.DownArrow))
        {
			if(currentHealth > 0f)
				currentHealth--;
			else
				Debug.Log("Player Die!");
        }
    }
}
