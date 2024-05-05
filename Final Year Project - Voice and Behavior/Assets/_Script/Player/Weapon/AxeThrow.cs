using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
	[SerializeField] private LayerMask aimColliderLayerMask;
    public bool activated;
    [SerializeField] private float rotationSpeed;

    void Update()
    {

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object's layer is included in the aimColliderLayerMask
		if (((1 << collision.gameObject.layer) & aimColliderLayerMask) != 0)
		{
			print(collision.gameObject.name);
			GetComponent<Rigidbody>().Sleep();
			GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			GetComponent<Rigidbody>().isKinematic = true;
			activated = false;
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Breakable"))
        {
            if(other.GetComponent<BreakBoxScript>() != null)
            {
                other.GetComponent<BreakBoxScript>().Break();
            }
        }*/
    }
}
