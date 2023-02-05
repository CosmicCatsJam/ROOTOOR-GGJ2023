using UnityEngine.Events;

public static class EventManager
{
    public static IntEvent OnUpsideDownWorldTransition = new IntEvent();
    public static UnityEvent OnLevelUp = new UnityEvent();
}

public class IntEvent : UnityEvent<int> { }






