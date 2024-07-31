using System;
using System.Collections;
using System.Collections.Generic;
using UniBT.Examples.Scripts;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    [SerializeField] GameObject enemywarning;

    // Start is called before the first frame update
    void Start()
    {
        enemywarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemywarning.SetActive(true);
        }

        else if (other.CompareTag("Player"))
        {
            enemywarning.SetActive(false);
        }
    }
}
