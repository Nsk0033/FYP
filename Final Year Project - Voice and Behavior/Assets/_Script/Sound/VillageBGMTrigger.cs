using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallHedge.SoundManager;

public class VillageBGMTrigger : MonoBehaviour
{
	void Start()
	{
		SoundType soundToPlay = SoundType.villageBGM;
		SoundManager.PlayRandomBGM(soundToPlay);
	}
	
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SoundType soundToPlay = SoundType.villageBGM;
			SoundManager.PlayRandomBGM(soundToPlay);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SoundType soundToPlay = SoundType.exploreBGM;
			SoundManager.PlayRandomBGM(soundToPlay);
		}
	}
}
