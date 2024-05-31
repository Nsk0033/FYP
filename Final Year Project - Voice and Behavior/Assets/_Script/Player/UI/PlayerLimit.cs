using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimit : MonoBehaviour
{
    [Header("Health")]
    public int maxLimit = 300;
    public int currentLimit;
    //[SerializeField] private gameObject healthBar;
	[SerializeField] private DuloGames.UI.UIProgressBar limitProgressBar;
	//private UIProgressBar uIProgressBar
	private float LimitPercentage = 0f;
	[SerializeField] private int increaseAmount = 1;
    [SerializeField] private float increaseInterval = 1f;
	[SerializeField] private float smoothSpeed = 8f;
	
	private void Start()
	{
		currentLimit = 0;
		limitProgressBar.fillAmount = LimitPercentage;
		StartCoroutine(IncreaseLimitOverTime());
		
	}
	
	private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.OnDealDamage.AddListener(OnDamageDealt);
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.OnDealDamage.RemoveListener(OnDamageDealt);
    }
	
	void Update()
    {
		LimitPercentage = (float)currentLimit / maxLimit;
		limitProgressBar.fillAmount = Mathf.Lerp(limitProgressBar.fillAmount, LimitPercentage, smoothSpeed * Time.deltaTime);
		//limitProgressBar.fillAmount = LimitPercentage;
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
			if(currentLimit != maxLimit)
				currentLimit = maxLimit;
		}
		if(currentLimit >= maxLimit)
		{
			currentLimit = maxLimit;
		}
    }
	
	private IEnumerator IncreaseLimitOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            if (currentLimit < maxLimit)
            {
                currentLimit += increaseAmount;
                if (currentLimit > maxLimit)
                {
                    currentLimit = maxLimit;
                }
            }
        }
    }
	
	public void GainLimit(int LimitAmount)
	{
		currentLimit += LimitAmount;
	}
	
	private void OnDamageDealt(int damage)
    {
        GainLimit(damage);
    }
}
