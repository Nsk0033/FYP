using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadLookAt : MonoBehaviour
{
    //[SerializeField] private Rig rig;
    //[SerializeField] private Transform headLookAtTransform;

    private bool isLookingAtPosition;

    /*private void Update() {
        float targetWeight = isLookingAtPosition ? 1f : 0f;
        //float lerpSpeed = 2f;
        //rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);
    }*/

    public void LookAtPosition(Vector3 lookAtPosition) {
        isLookingAtPosition = true;
        Vector3 direction = (lookAtPosition - transform.position).normalized;

        // Ignore changes in x and z to only rotate around y-axis
        direction.y = 0; 
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float lerpSpeed = 200f;
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lerpSpeed);
        
        // Only apply the y rotation
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }
}
