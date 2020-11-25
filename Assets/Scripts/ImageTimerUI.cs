using UnityEngine.UI;
using UnityEngine;

public class ImageTimerUI : TimerUI
{
    [SerializeField] private Image _timeImage = null;

    private void Awake()
    {
        if (_timeImage == null)
            SetReferences();
    }

    protected override void UpdateUI()
    {
        _timeImage.fillAmount = GetRemainingNormalizedTime();
    }

    [ContextMenu("Set References", false, 0)]
    protected override void SetReferences()
    {
        base.SetReferences();
        _timeImage = transform.Find(nameof(_timeImage)).GetComponent<Image>();
    }
}