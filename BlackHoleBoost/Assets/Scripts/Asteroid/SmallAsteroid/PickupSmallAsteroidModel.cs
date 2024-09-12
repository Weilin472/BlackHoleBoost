using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/11/2024]
 * [changes models depending on asteroid]
 */

public class PickupSmallAsteroidModel : MonoBehaviour
{
    private PickupSmallAsteroidEventBus _pickupSmallAsteroidEventBus;

    [SerializeField] private GameObject _normalModel;
    [SerializeField] private GameObject _bounceModel;
    [SerializeField] private GameObject _stickyModel;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _pickupSmallAsteroidEventBus = GetComponent<PickupSmallAsteroidEventBus>();
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
        _normalModel.SetActive(false);
        _bounceModel.SetActive(false);
        _stickyModel.SetActive(false);
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetNormal);
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _pickupSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// sets model to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _normalModel.SetActive(true);
    }

    /// <summary>
    /// sets model to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _bounceModel.SetActive(true);
    }

    /// <summary>
    /// sets model to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _stickyModel.SetActive(true);
    }
}
