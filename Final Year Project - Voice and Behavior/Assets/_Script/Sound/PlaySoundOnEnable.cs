using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallHedge.SoundManager;

public class PlaySoundOnEnable : MonoBehaviour
{
	[SerializeField] private SoundType sound;
	[SerializeField, Range(0, 1)] private float volume = 1;
	
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound(sound, null, volume);
    }
}
