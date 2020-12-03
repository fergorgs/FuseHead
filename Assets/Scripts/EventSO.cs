using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Global Event", menuName = "Events/Action Event")]
public class EventSO : ScriptableObject
{
    public UnityEvent Event = default;

    public void Subscribe(UnityAction call)
    {
        Event.AddListener(call);
    }

    public void Unsubscribe(UnityAction call)
    {
        Event.RemoveListener(call);
    }

    public void Invoke()
    {
        Event.Invoke();
    }
}
