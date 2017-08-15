using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher {

    internal static string OnPickMoneyBag = "OnPickMoneyBag";

    internal delegate void EventListener(Object param);

    private static readonly EventDispatcher _instance = new EventDispatcher();

    private Dictionary<string, EventListener> EventMap = new Dictionary<string, EventListener>(); 

    public static EventDispatcher Instance
    {
        get{
            return _instance;
        }
    }


    private EventDispatcher()
    {

    }


    internal void AddEventListener(string Type, EventListener listener)
    {
        EventListener listeners;
        EventMap.TryGetValue(Type, out listeners);
        if (listeners == null)
            listeners = listener;
        else
            listeners += listener;
        EventMap.Add(Type, listeners);
    }


    internal void DispatcherEvent(string Type, Object param = null)
    {
        EventListener listeners;
        EventMap.TryGetValue(Type, out listeners);
        if (listeners != null)
            listeners(param);
    }
}
