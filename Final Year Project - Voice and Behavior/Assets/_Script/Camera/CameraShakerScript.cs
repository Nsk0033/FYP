using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CameraShakerScript : MonoBehaviour
{
	public ShakeData MyShakeData;
    // Start is called before the first frame update
    void Start()
    {
        CameraShakerHandler.Shake(MyShakeData);
    }

}
