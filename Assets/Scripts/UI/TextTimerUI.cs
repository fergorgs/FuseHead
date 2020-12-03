using TMPro;
using UnityEngine;

public class TextTimerUI : TimerUI
{
    [SerializeField] private TextMeshProUGUI _timeText = null;

    private void Awake()
    {
        if (_timeText == null)
            SetReferences();
    }

    protected override void UpdateUI()
    {
        _timeText.SetText(GetRemainingTime().ToString("F1"));
    }

    [ContextMenu("Set References", false, 0)]
    protected override void SetReferences()
    {
        base.SetReferences();
        _timeText = transform.Find(nameof(_timeText)).GetComponent<TextMeshProUGUI>();
    }
}
