using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClone : MonoBehaviour
{
	[SerializeField] private Transform SpawnLocation;
	
	private void OnEnable()
	{
		transform.position = SpawnLocation.position;
		transform.rotation = SpawnLocation.rotation;
	}
}
