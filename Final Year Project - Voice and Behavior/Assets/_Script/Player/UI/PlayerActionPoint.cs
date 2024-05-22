using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionPoint : MonoBehaviour
{
    [Header("Action Point")]
    [SerializeField] private float maxActionPoint = 100f;
	[Range(0f, 100.1f)]
    [SerializeField] private float currentActionPointValue;
    [SerializeField] private int currentActionPointAvailable;
    //[SerializeField] private gameObject healthBar;
	[SerializeField] private DuloGames.UI.UIProgressBar actionPointProgressBar;
	[SerializeField] private GameObject actionPointFill1;
	[SerializeField] private GameObject actionPointFill2;
	[SerializeField] private GameObject actionPointFill3;
	[SerializeField] private GameObject actionPointFill4;
	//private UIProgressBar uIProgressBar
	private float APPercentage = 0f;
	
	private void Awake()
	{
		//uIProgressBar = healthBar.GetComponent<UIProgressBar>();
	}
	
	private void Start()
	{
		currentActionPointValue = 0f;
		currentActionPointAvailable = 0;
		actionPointProgressBar.fillAmount = APPercentage;
	}
	
	void Update()
    {
		APPercentage = (float)currentActionPointValue / maxActionPoint;
		actionPointProgressBar.fillAmount = APPercentage;
		switch(currentActionPointAvailable)
		{
			case 0:
                actionPointFill1.SetActive(false);
                actionPointFill2.SetActive(false);
                actionPointFill3.SetActive(false);
                actionPointFill4.SetActive(false);
                break;
            case 1:
                actionPointFill1.SetActive(true);
                actionPointFill2.SetActive(false);
                actionPointFill3.SetActive(false);
                actionPointFill4.SetActive(false);
                break;
            case 2:
                actionPointFill1.SetActive(true);
                actionPointFill2.SetActive(true);
                actionPointFill3.SetActive(false);
                actionPointFill4.SetActive(false);
                break;
            case 3:
                actionPointFill1.SetActive(true);
                actionPointFill2.SetActive(true);
                actionPointFill3.SetActive(true);
                actionPointFill4.SetActive(false);
                break;
            case 4:
                actionPointFill1.SetActive(true);
                actionPointFill2.SetActive(true);
                actionPointFill3.SetActive(true);
                actionPointFill4.SetActive(true);
                break;
            default:
                actionPointFill1.SetActive(false);
                actionPointFill2.SetActive(false);
                actionPointFill3.SetActive(false);
                actionPointFill4.SetActive(false);
                break;
        }
		if(currentActionPointValue >= 100f)
		{
			if(currentActionPointAvailable >= 4)
			{
				currentActionPointValue = 99f;
			}
			else
			{
				currentActionPointAvailable++;
				currentActionPointValue = 0f;
			}
		}
	
		
		
        // Example: increase the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentActionPointValue++;
        }

        // Example: decrease the fill amount by 0.01 per frame
        if (Input.GetKey(KeyCode.DownArrow))
        {
			if(currentActionPointValue > 0)
				currentActionPointValue--;
        }
		if (Input.GetKey(KeyCode.O))
        {
			if(currentActionPointAvailable > 0)
				currentActionPointAvailable--;
        }
    }
}
