using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallHedge.SoundManager;

public class PortalTeleport : MonoBehaviour
{
	[SerializeField] private Transform teleportDestination;
    [SerializeField] private float teleportTime = 0.1f; // Time taken to teleport

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Something Entered");
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
			Debug.Log("Teleport Player");
            // Start the teleportation coroutine
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private IEnumerator TeleportPlayer(Transform playerTransform)
    {
        Vector3 startPosition = playerTransform.position;
        Vector3 endPosition = teleportDestination.position;
        float elapsedTime = 0f;
		
		SoundType soundToPlay = SoundType.PORTAL;
		SoundManager.PlaySound(soundToPlay, null, 0.8f);
		
        // Smoothly move the player to the teleport destination over time
        while (elapsedTime < teleportTime)
        {
            // Calculate the interpolation value (0 to 1)
            float t = elapsedTime / teleportTime;

            // Interpolate between start and end positions
            playerTransform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Yield and wait for the next frame
            yield return null;
        }

        // Ensure the player is exactly at the teleport destination
        playerTransform.position = endPosition;
    }
}
