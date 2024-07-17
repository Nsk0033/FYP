using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 20;
    [SerializeField] private float damageInterval = 1f;
    private bool isPlayerInLava = false;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInLava = true;
            damageCoroutine = StartCoroutine(DamagePlayerOverTime(other.GetComponent<PlayerHealth>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInLava = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DamagePlayerOverTime(PlayerHealth playerHealth)
    {
        while (isPlayerInLava)
        {
            playerHealth.DamagePlayer(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
