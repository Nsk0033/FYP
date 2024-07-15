using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteractableDialogOnly : MonoBehaviour, IInteractable
{
	[Header("Conversation Related Panel")]
    [SerializeField] private GameObject wholePanel; // whole panel
    [SerializeField] private TextMeshProUGUI textContent; // the TMP need to be generated
    [SerializeField] private string[] lines; // all needed conversation
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private string interactText;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;
    private int index;
    private bool isTyping = false;
    private bool isDisplaying = false;
    //private bool wasIconActive = false; // To track the previous state of the icon

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }


    public void Interact(Transform interactorTransform)
    {
        if (!wholePanel.activeSelf)
        {
            StartConversation(interactorTransform);
        }
        else if (isTyping)
        {
            StopAllCoroutines();
            textContent.text = lines[index];
            isTyping = false;
            isDisplaying = true;
        }
        else if (!isTyping && isDisplaying)
        {
            AdvanceDialogue();
        }
    }

    private void StartConversation(Transform interactorTransform)
    {
		PlayerStateManager.instance.SetCanMove(false);
        animator.SetBool("Talking", true);
        //Invoke("StopTalking", 3f);
        float playerHeight = 1.8f;
        npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
        wholePanel.SetActive(true);
        index = 0;
        DisplayNextLine();
    }

    private void AdvanceDialogue()
    {
        if (textContent.text == lines[index])
        {
            index++;
            if (index < lines.Length)
            {
                DisplayNextLine();
            }
            else
            {
                EndConversation();

            }
        }
        else
        {
            StopAllCoroutines();
            textContent.text = lines[index];
        }
    }

    private void DisplayNextLine()
    {
        textContent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        isDisplaying = false;
        foreach (char c in lines[index].ToCharArray())
        {
            textContent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
        isDisplaying = true;
    }

    private void EndConversation()
    {
		PlayerStateManager.instance.SetCanMove(true);
        wholePanel.SetActive(false);
		ResetRotation();
        index = 0;
		animator.SetBool("Talking", false);
    }
	
	private void ResetRotation()
    {
        npcHeadLookAt.ReturnToInitialRotation();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
