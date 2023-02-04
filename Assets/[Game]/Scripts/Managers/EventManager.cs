using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnUpsideDownWorldTransition = new UnityEvent();
}

public class IntEvent : UnityEvent<float> { }






