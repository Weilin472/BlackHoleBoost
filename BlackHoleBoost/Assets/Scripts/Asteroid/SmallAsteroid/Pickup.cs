using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [pick up for when player flies over item]
 */

public class Pickup : MonoBehaviour
{
    private PickupSmallAsteroidEventBus _pickupSmallAsteroidEventBus;
    private PickupSmallAsteroid _pickupSmallAsteroid;

    private SmallAsteroidType _type = SmallAsteroidType.NORMAL;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _pickupSmallAsteroidEventBus = GetComponent<PickupSmallAsteroidEventBus>();
        _pickupSmallAsteroid = GetComponent<PickupSmallAsteroid>();
    }

    /// <summary>
    /// subscribes to event bus
    /// </summary>
    private void OnEnable()
    {
        _pickupSmallAsteroidEventBus.Subscribe(SmallAsteroidType.NORMAL, SetNormal);
        _pickupSmallAsteroidEventBus.Subscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _pickupSmallAsteroidEventBus.Subscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// disables models and unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetNormal);
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<PlayerAsteroidInventory>())
        {
            PlayerAsteroidInventory inventory = other.transform.root.gameObject.GetComponent<PlayerAsteroidInventory>();
            inventory.AddAsteroid(_type);
            _pickupSmallAsteroid.ReturnToPool();
        }
    }

    /// <summary>
    /// sets type to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _type = SmallAsteroidType.NORMAL;
    }

    /// <summary>
    /// sets type to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _type = SmallAsteroidType.BOUNCE;
    }

    /// <summary>
    /// sets type to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _type = SmallAsteroidType.STICKY;
    }
}
