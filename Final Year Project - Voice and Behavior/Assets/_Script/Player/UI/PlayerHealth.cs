using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using EmeraldAI.CharacterController;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 300;
    [SerializeField] private GameObject healVFX;
    public int CurrentHealth;
    [SerializeField] private DuloGames.UI.UIProgressBar healthProgressBar;
    private float HPPercentage = 0f;
    public bool canDamage = true;

    public static PlayerHealth instance { get; set; }
    
    private ThirdPersonController thirdPersonController;
    private bool isDead = false; // Flag to track if the player is dead

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("More than one PlayerHealth instance found!");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        thirdPersonController = GetComponent<ThirdPersonController>();
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
        
        if (CurrentHealth <= 0 && !isDead) // Check if the player is dead and call PlayerDeath once
        {
            CurrentHealth = 0;
            PlayerDeath();
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
        if (canDamage && !isDead) // Only apply damage if the player is not dead
        {
            CurrentHealth -= damageInput;
            Debug.Log("Ooof");
        }    
    }
    
    public void HealPlayer(int healInput)
    {    
        if (!isDead) // Only heal if the player is not dead
        {
            CurrentHealth += healInput;
        }
    }
    
    public void PlayerDeath()
    {    
        isDead = true; // Set the flag to true
        thirdPersonController.DeadAnimationTrigger();
        Invoke("PlayerRespawn", 3f);
    }
    
    public void PlayerRespawn()
    {    
        thirdPersonController.RespawnAnimationTrigger();
        CurrentHealth = maxHealth / 3;
		Instantiate(healVFX,transform.position,transform.rotation);
        isDead = false; // Reset the flag to false
    }
}
