using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EV
{
    None,
    RecupObjet,

}



public delegate void EventHandler(params object[] args);

/**
    * Singleton enabling decoupling managers
*/


public class EventManager
{
    private static EventManager _instance;
    private Dictionary<string, EventHandler> _events;

    private EventManager()
    {
        _events = new Dictionary<string, EventHandler>();
    }

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }

            return _instance;
        }
    }



    public void Subscribe(EV eventId, EventHandler handler)
    {
        Subscribe(eventId.ToString(), handler);
    }


    public void Subscribe(string eventId, EventHandler handler)
    {
        if (!_events.ContainsKey(eventId))
        {
            _events[eventId] = null;
        }

        _events[eventId] += handler;
    }

    internal void UnsubscribeAll()
    {
        _events.Clear();
    }

    public void Unsubscribe(EV eventId, EventHandler handler)
    {
        Unsubscribe(eventId.ToString(), handler);
    }


    public void Unsubscribe(string eventId, EventHandler handler)
    {
        if (_events.ContainsKey(eventId))
        {
            _events[eventId] -= handler;
        }
    }


    public void Trigger(EV eventId, params object[] args)
    {
        Trigger(eventId.ToString(), args);
    }


    public void Trigger(string eventId, params object[] args)
    {
        if (_events.ContainsKey(eventId))
        {
            EventHandler handlers = _events[eventId];
            if (handlers != null)
            {
                handlers(args);
            }
        }
    }
}