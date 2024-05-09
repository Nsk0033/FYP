using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamageable
{
    public void Damage()
	{
		Debug.Log("Enemy Hit!");
	}
}
