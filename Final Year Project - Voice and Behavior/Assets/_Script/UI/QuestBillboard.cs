using UnityEngine;
using TMPro;

public class QuestBillboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private string questId;

    public void SetQuestInfo(string questId, QuestStepState newState)
    {
        this.questId = questId;
        UpdateQuestState(newState);
    }

    public void UpdateQuestState(QuestStepState newState)
    {
        titleText.text = questId;
        descriptionText.text = newState.state + newState.status;
    }
}
