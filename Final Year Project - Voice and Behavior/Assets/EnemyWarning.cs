using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (this.tag == "Companion" && other.CompareTag("Enemy"))
        {
            enemywarning.active = true;
        }

        else
        {
            enemywarning.active = false;
        }
    }
}
