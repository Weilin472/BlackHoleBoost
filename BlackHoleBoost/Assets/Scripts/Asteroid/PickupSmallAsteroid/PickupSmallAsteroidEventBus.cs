using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [event bus for different pick upable asteroids]
 */

public class PickupSmallAsteroidEventBus : MonoBehaviour
{
    private readonly IDictionary<SmallAsteroidType, UnityEvent> Events = new Dictionary<SmallAsteroidType, UnityEvent>();

    /// <summary>
    /// adds function to events
    /// </summary>
    /// <param name="eventType">what event</param>
    /// <param name="listener">function</param>
    public void Subscribe(SmallAsteroidType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    /// <summary>
    /// remove function to events
    /// </summary>
    /// <param name="type">what event</param>
    /// <param name="listener">function</param>
    public void Unsubscribe(SmallAsteroidType type, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// invokes/runs functions for event
    /// </summary>
    /// <param name="type">event</param>
    public void Publish(SmallAsteroidType type)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
