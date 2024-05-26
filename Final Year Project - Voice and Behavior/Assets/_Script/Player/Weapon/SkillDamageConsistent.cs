using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class SkillDamageConsistent : MonoBehaviour
{
    [SerializeField] private int damageOutput = 10;
	[SerializeField] private float detectionInterval = 0.1f; // Time between detections
    [SerializeField] private float detectionDuration = 1f; // Total duration of detection
    [SerializeField] private float startDelay = 0.5f; // Delay before starting detection

    private Collider skillCollider;

    private void Start()
    {
        skillCollider = GetComponent<Collider>();
        skillCollider.enabled = false;
        StartCoroutine(StartDetectionAfterDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (skillCollider.enabled && other.GetComponent<EmeraldAISystem>() != null)
        {
            Debug.Log("Object hit with skill!");
            other.GetComponent<EmeraldAISystem>().Damage(damageOutput, EmeraldAISystem.TargetType.Player, transform, 400);
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
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < detectionDuration)
        {
            skillCollider.enabled = true;
            yield return new WaitForSeconds(detectionInterval);
            skillCollider.enabled = false;
            elapsedTime += detectionInterval;
        }
    }
}




