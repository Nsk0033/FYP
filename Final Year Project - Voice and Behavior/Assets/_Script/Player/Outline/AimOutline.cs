using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class AimOutline : MonoBehaviour
{
	[SerializeField] private float rayCastRange = 999f;
	[SerializeField] private GameObject objectDetected;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	private StarterAssetsInputs starterAssetsInputs;
	
    // Start is called before the first frame update
    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (starterAssetsInputs.aim) //only detect when aiming
		{
			DetectObject();
		}
		else
		{
			if (objectDetected != null)
			{
				DetectionChecking();
			}
			
		}
    }
	
	private void DetectObject()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		
		if (Physics.Raycast(ray,out RaycastHit raycastHit, rayCastRange, aimColliderLayerMask))
		{
			if (objectDetected == null)
			{
				objectDetected = raycastHit.collider.gameObject;
				if (objectDetected.GetComponent<OutlineQ>() != null)
				{
					objectDetected.GetComponent<OutlineQ>().enabled = true;
				}
				
			}
			else if (objectDetected != raycastHit.collider.gameObject)
			{
				objectDetected = raycastHit.collider.gameObject;
				if (objectDetected.GetComponent<OutlineQ>() != null)
				{
					objectDetected.GetComponent<OutlineQ>().enabled = true;
				}
				
			}
		}
		else
		{
			if(objectDetected != null)
			{
				if (objectDetected.GetComponent<OutlineQ>() != null)
				{
					objectDetected.GetComponent<OutlineQ>().enabled = false;
				}
				
				if (objectDetected.GetComponent<OutlineMarking>() != null)
				{
					objectDetected.GetComponent<OutlineMarking>().enabled = true;
				}
				
				objectDetected = null;
			}
		}
	}
	
	private void DetectionChecking()
	{
		if (objectDetected.GetComponent<OutlineQ>() != null)
		{
			objectDetected.GetComponent<OutlineQ>().enabled = false;
		}
		
		if (objectDetected.GetComponent<OutlineMarking>() != null)
		{
			objectDetected.GetComponent<OutlineMarking>().enabled = true;
		}
		
		objectDetected = null;
	}
}
