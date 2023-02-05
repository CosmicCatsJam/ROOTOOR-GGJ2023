using UnityEngine.Events;

public static class EventManager
{
    public static IntEvent OnUpsideDownWorldTransition = new IntEvent();
}

public class IntEvent : UnityEvent<int> { }






