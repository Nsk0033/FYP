using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallHedge.SoundManager;

public class EnemyBGMTrigger : MonoBehaviour
{
    [SerializeField] private SoundType enemyBGM;
    [SerializeField] private SoundType exploreBGM;

    private bool hasEnemiesInRange = false;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider is missing.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hasEnemiesInRange = true;
            SoundManager.PlayRandomBGM(enemyBGM);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(
            capsuleCollider.bounds.center - capsuleCollider.transform.up * capsuleCollider.height / 2,
            capsuleCollider.bounds.center + capsuleCollider.transform.up * capsuleCollider.height / 2,
            capsuleCollider.radius,
            LayerMask.GetMask("Enemy"));

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hasEnemiesInRange = true;
                return;
            }
        }

        hasEnemiesInRange = false;
        SoundManager.PlayRandomBGM(exploreBGM);
    }

    private void Update()
    {
        if (hasEnemiesInRange)
        {
            CheckForEnemies();
        }
    }
}
