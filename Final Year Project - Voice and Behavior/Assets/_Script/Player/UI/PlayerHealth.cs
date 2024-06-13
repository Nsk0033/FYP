using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using EmeraldAI.CharacterController;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 300;
    public int CurrentHealth;
    [SerializeField] private DuloGames.UI.UIProgressBar healthProgressBar;
    private float HPPercentage = 0f;
	public bool canDamage = true;

    public static PlayerHealth instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("More than one PlayerHealth instance found!");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject); // Optional: If you want the PlayerHealth to persist across scenes
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        healthProgressBar.fillAmount = HPPercentage;
    }

    void Update()
    {
        HPPercentage = (float)CurrentHealth / maxHealth;
        healthProgressBar.fillAmount = HPPercentage;
		
		if (CurrentHealth >= maxHealth)
		{
			CurrentHealth = maxHealth;
		}
		
		if (CurrentHealth <= 0)
		{
			CurrentHealth = 0;
		}
		
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (CurrentHealth < maxHealth)
                CurrentHealth++;
            else
                CurrentHealth = maxHealth;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (CurrentHealth > 0f)
                CurrentHealth--;
            else
                Debug.Log("Player Die!");
        }
    }

    public void DamagePlayer(int damageInput)
    {
		if(canDamage)
		{
			CurrentHealth -= damageInput;
			Debug.Log("Ooof");
		}    
    }
	
	public void HealPlayer(int healInput)
    {	
		CurrentHealth += healInput;
    }
}
