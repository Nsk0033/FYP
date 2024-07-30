using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFreezingScript : MonoBehaviour
{
    [SerializeField] private Material frozenMaterial; // Material to change to
    [SerializeField] private float transitionDuration = 1f; // Duration of the transition
    [SerializeField] private GameObject cinemachine; // Duration of the transition
    public bool isChanging; 

    private Material originalMaterial; // Original material of the object
    private Renderer objectRenderer; // Renderer of the object

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material; // Store the original material
    }

    public void Freeze()
    {
        StartCoroutine(SmoothTransitionToFrozenMaterial());
		isChanging = true;
		cinemachine.SetActive(true);
		Invoke("StopCinemachine",2.5f);
    }
	
	private void StopCinemachine()
	{
		cinemachine.SetActive(false);
	}

    private IEnumerator SmoothTransitionToFrozenMaterial()
    {
        float elapsedTime = 0f;

        // Store the initial color of the original material and the target material
        Color originalColor = GetMaterialColor(originalMaterial);
        Color targetColor = GetMaterialColor(frozenMaterial);

        // Loop until the transition is complete
        while (elapsedTime < transitionDuration)
        {
            // Calculate the current interpolation factor
            float t = elapsedTime / transitionDuration;

            // Lerp the color of the material based on the interpolation factor
            SetMaterialColor(objectRenderer.material, Color.Lerp(originalColor, targetColor, t));

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final material is set to the frozen material at the end of the transition
        objectRenderer.material = frozenMaterial;

    }

    private Color GetMaterialColor(Material mat)
    {
        // Check for common color properties
        if (mat.HasProperty("_Color"))
        {
            return mat.color;
        }
        if (mat.HasProperty("_BaseColor"))
        {
            return mat.GetColor("_BaseColor");
        }
        if (mat.HasProperty("_TintColor"))
        {
            return mat.GetColor("_TintColor");
        }

        // Default to white if no known color property is found
        return Color.white;
    }

    private void SetMaterialColor(Material mat, Color color)
    {
        // Set the appropriate color property
        if (mat.HasProperty("_Color"))
        {
            mat.color = color;
        }
        else if (mat.HasProperty("_BaseColor"))
        {
            mat.SetColor("_BaseColor", color);
        }
        else if (mat.HasProperty("_TintColor"))
        {
            mat.SetColor("_TintColor", color);
        }
    }
}