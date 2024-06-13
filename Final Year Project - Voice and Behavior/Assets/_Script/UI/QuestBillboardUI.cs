using System.Collections.Generic;
using UnityEngine;

public class QuestBillboardUI : MonoBehaviour
{
    public GameObject questUIPrefab; // Prefab for quest UI
    public Transform questUIParent; // Parent transform for the UI elements
    private Dictionary<string, GameObject> questUIDictionary = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStepStateChange += HandleQuestStepStateChange;
        GameEventsManager.instance.questEvents.onFinishQuest += HandleQuestAdvance;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= HandleQuestStepStateChange;
        GameEventsManager.instance.questEvents.onFinishQuest -= HandleQuestAdvance;
    }

    private void HandleQuestStepStateChange(string questId, int stepIndex, QuestStepState newState)
    {
        if (questUIDictionary.ContainsKey(questId))
        {
            UpdateQuestUI(questId, newState);
        }
        else
        {
            CreateQuestUI(questId, newState);
        }
    }

    private void HandleQuestAdvance(string questId)
    {
        if (questUIDictionary.ContainsKey(questId))
        {
            Destroy(questUIDictionary[questId]);
            questUIDictionary.Remove(questId);
        }
    }

    private void CreateQuestUI(string questId, QuestStepState newState)
    {
        GameObject questUIInstance = Instantiate(questUIPrefab, questUIParent);
        QuestBillboard questBillboard = questUIInstance.GetComponent<QuestBillboard>();
        if (questBillboard != null)
        {
            questBillboard.SetQuestInfo(questId, newState);
            questUIDictionary.Add(questId, questUIInstance);
        }
    }

    private void UpdateQuestUI(string questId, QuestStepState newState)
    {
        if (questUIDictionary.TryGetValue(questId, out GameObject questUIInstance))
        {
            QuestBillboard questBillboard = questUIInstance.GetComponent<QuestBillboard>();
            if (questBillboard != null)
            {
                questBillboard.UpdateQuestState(newState);
            }
        }
    }
}