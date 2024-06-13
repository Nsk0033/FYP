using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHeal : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
	[SerializeField] private float startDelay = 0.5f; // Delay before starting detection
	
    private bool isDetecting = false;
    private Collider skillCollider;

    private void Start()
    {
        skillCollider = GetComponent<Collider>();
        if (skillCollider != null)
        {
            skillCollider.enabled = false;
        }
        StartCoroutine(StartDetectionAfterDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDetecting)
            return;

        Debug.Log("Object hit with skill!");

        if (other.CompareTag("player"))
		{
			if (PlayerHealth.instance != null)
			{
				// Call HealPlayer function with a specified healing amount
				PlayerHealth.instance.HealPlayer(healAmount);
			}
			else
			{
				Debug.LogWarning("PlayerHealth instance not found!");
			}
		}
    }

    private IEnumerator StartDetectionAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        if (skillCollider != null)
        {
            skillCollider.enabled = true;
        }
        isDetecting = true;
    }
}
