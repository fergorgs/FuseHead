using TMPro;
using UnityEngine;

public class TextTimerUI : TimerUI
{
    [SerializeField] private TextMeshProUGUI _timeText = null;
    [SerializeField] private string stringFormat = "F1";

    private void Awake()
    {
        if (_timeText == null)
            SetReferences();
    }

    protected override void UpdateUI()
    {
        _timeText.SetText(GetRemainingTime().ToString(stringFormat));
    }

    [ContextMenu("Set References", false, 0)]
    protected override void SetReferences()
    {
        base.SetReferences();
        _timeText = transform.Find(nameof(_timeText)).GetComponent<TextMeshProUGUI>();
    }
}
