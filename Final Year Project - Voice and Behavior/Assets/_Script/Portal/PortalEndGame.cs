using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalEndGame : MonoBehaviour
{
    [SerializeField] private GameObject confirmationCanvas; // Reference to the confirmation canvas
    [SerializeField] private Button confirmButton; // Reference to the confirm button
    [SerializeField] private Button cancelButton; // Reference to the cancel button
    [SerializeField] private string nextSceneName; // Name of the next scene to load

    private void Start()
    {
        // Ensure the canvas is hidden initially
        if (confirmationCanvas != null)
        {
            confirmationCanvas.SetActive(false);
        }

        // Add listeners to the buttons
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirm);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(OnCancel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the confirmation canvas when player enters the trigger
            if (confirmationCanvas != null)
            {
                confirmationCanvas.SetActive(true);
            }
        }
    }

    private void OnConfirm()
    {
        // Load the next scene when confirm is clicked
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnCancel()
    {
        // Hide the confirmation canvas when cancel is clicked
        if (confirmationCanvas != null)
        {
            confirmationCanvas.SetActive(false);
        }
    }
}