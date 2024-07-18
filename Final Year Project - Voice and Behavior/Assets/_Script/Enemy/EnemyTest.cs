using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamageable
{
	[SerializeField] private float disableTime = 3f;
	
    public void Damage()
	{
		Debug.Log("Enemy Hit!");
	}
	
	private void DisableObject()
	{
		gameObject.SetActive(false);
	}
	
	public void StartDisableObject()
	{
		Invoke("DisableObject",disableTime);
	}
	
	private void DestroyObject()
	{
		Destroy(gameObject);
	}
	
	public void StartDestroyObject()
	{
		Invoke("DestroyObject",disableTime);
	}
}
