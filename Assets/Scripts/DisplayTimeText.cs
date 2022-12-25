using UnityEngine;
using TMPro;

public class DisplayTimeText : MonoBehaviour
{
    [SerializeField] private Timer timer = null;
    private TextMeshProUGUI timeText = default;

    private void Awake()
    {
        if(timer == null)
            timer = FindObjectOfType<Timer>();
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        timeText.SetText(timer.GetTimeElapsed().ToString("F1"));
    }

    private void OnDisable()
    {
        timeText.SetText(string.Empty);
    }
}