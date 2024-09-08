using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/07/2024]
 * [event bus for setting asteroid size]
 */

public class EnemyAsteroidEventBus : MonoBehaviour
{
    private readonly IDictionary<EnemyAsteroidSizeEnum, UnityEvent> Events = new Dictionary<EnemyAsteroidSizeEnum, UnityEvent>();

    /// <summary>
    /// adds function to events
    /// </summary>
    /// <param name="eventType">what event</param>
    /// <param name="listener">function</param>
    public void Subscribe(EnemyAsteroidSizeEnum eventType, UnityAction listener)
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
    public void Unsubscribe(EnemyAsteroidSizeEnum type, UnityAction listener)
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
    public void Publish(EnemyAsteroidSizeEnum type)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
