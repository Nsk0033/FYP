using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisappear : MonoBehaviour
{
	[SerializeField] private float destroyTime = 2.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the game object after 'destroyTime' seconds
        Destroy(gameObject, destroyTime);
    }

    
}
