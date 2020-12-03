using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Global Event", menuName = "Events/Boolean Event")]
public class BooleanEventSO : ScriptableObject
{
    public class BooleanUnityEvent : UnityEvent<bool> { }

    public BooleanUnityEvent Event = new BooleanUnityEvent();
    public bool lastCall = default;

    private void OnEnable()
    {
        lastCall = false;
    }

    public void Subscribe(UnityAction<bool> call)
    {
        Event.AddListener(call);
    }

    public void Unsubscribe(UnityAction<bool> call)
    {
        Event.RemoveListener(call);
    }

    public void Invoke(bool value)
    {
        Event.Invoke(value);
        lastCall = value;
        Debug.Log(name + value);
    }
}
