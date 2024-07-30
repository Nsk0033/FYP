using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractWIthChild : QuestStep, IInteractable
{
	[Header("Conversation Related Panel")]
    [SerializeField] private GameObject wholePanel; // whole panel
    [SerializeField] private TextMeshProUGUI textContent; // the TMP need to be generated
    [SerializeField] private string[] lines; // all needed conversation
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private string interactText;
	
	[Header("Cinemachine Related Panel")]
    [SerializeField] private GameObject cinemachine;
	
	[Header("Quest Related Panel")]
	[SerializeField] private GameObject childDuck;
	[SerializeField] private GameObject childStand;
	[SerializeField] private Transform childLocation1;
	[SerializeField] private Transform childLocation2;
	
	private int index;
    private bool isTyping = false;
    private bool isDisplaying = false;
	private GameObject spawnedObject;
	
    private void Start()
    {
        string status = "Interact with child.";
        ChangeState("", status);
		spawnedObject = Instantiate(childDuck,childLocation1.position,childLocation1.rotation);
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
        wholePanel.SetActive(true);
        index = 0;
        DisplayNextLine();
		cinemachine.SetActive(true);
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
        index = 0;
		string status = "Bring the child back to his mother";
        ChangeState("", status);
		cinemachine.SetActive(false);
		Invoke("FinishQuestStep",0.2f);
		Invoke("HideChild",0.2f);
        //FinishQuestStep();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
	
	private void HideChild()
	{
		PlayerStateManager.instance.SetCanMove(true);
		Destroy(spawnedObject);
		Instantiate(childStand,childLocation2.position,childLocation2.rotation);
	}

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}