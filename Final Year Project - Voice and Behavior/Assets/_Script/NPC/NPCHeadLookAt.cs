using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadLookAt : MonoBehaviour
{
    private bool isLookingAtPosition;
    private Quaternion initialRotation;
    private bool hasRecordedInitialRotation = false;

    private void Start()
    {
        RecordInitialRotation();
    }

    private void RecordInitialRotation()
    {
        initialRotation = transform.rotation;
        hasRecordedInitialRotation = true;
    }

    public void LookAtPosition(Vector3 lookAtPosition)
    {
        isLookingAtPosition = true;
        Vector3 direction = (lookAtPosition - transform.position).normalized;

        // Ignore changes in x and z to only rotate around y-axis
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float lerpSpeed = 150f;
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lerpSpeed);

        // Only apply the y rotation
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    public void ReturnToInitialRotation()
    {
        if (hasRecordedInitialRotation)
        {
            float lerpSpeed = 150f;
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * lerpSpeed);

            // Only apply the y rotation
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
    }
}
