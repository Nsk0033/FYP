using UnityEngine;
using UnityEngine.UI;

public class IndicatorColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject canFinishIcon;
    [SerializeField] private Color startColor = Color.yellow; // Color when canStartIcon is active
    [SerializeField] private Color finishColor = Color.cyan; // Color when canFinishIcon is active

    private Color originalColor; // Original color
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("No Image component found on this GameObject.");
        }

        // Set original color from hex code
        if (ColorUtility.TryParseHtmlString("#00FF68FF", out originalColor))
        {
            // Successfully parsed color
        }
        else
        {
            Debug.LogError("Failed to parse original color hex code.");
        }
    }

    void Update()
    {
        if (image != null)
        {
            if (canStartIcon.activeSelf)
            {
                image.color = startColor;
            }
            else if (canFinishIcon.activeSelf)
            {
                image.color = finishColor;
            }
            else
            {
                image.color = originalColor;
            }
        }
    }
}
