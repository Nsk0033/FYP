using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class SkillDamage : MonoBehaviour
{
    [SerializeField] private int damageOutput = 10;
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

        var emeraldAI = other.GetComponent<EmeraldAI.EmeraldAISystem>();
        if (emeraldAI != null)
        {
            emeraldAI.Damage(damageOutput, EmeraldAI.EmeraldAISystem.TargetType.Player, transform, 400);
            DealDamage();
        }
    }

    public void DealDamage()
    {
        GameEventsManager.instance.playerEvents.OnDealDamage.Invoke(damageOutput);
        Debug.Log($"Skill Hit! Damage Output: {damageOutput} from {gameObject.name}");
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


