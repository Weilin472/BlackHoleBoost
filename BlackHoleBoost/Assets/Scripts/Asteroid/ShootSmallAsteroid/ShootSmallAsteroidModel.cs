using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/16/2024]
 * [Sets models for shoot asteroid]
 */

public class ShootSmallAsteroidModel : MonoBehaviour
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;

    [SerializeField] private GameObject _normalModel;
    [SerializeField] private GameObject _bounceModel;
    [SerializeField] private GameObject _stickyModel;

    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
    }

    // <summary>
    /// subscribes to event bus
    /// </summary>
    private void OnEnable()
    {
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.NORMAL, SetNormal);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _shootSmallAsteroidEventBus.Subscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// unsubscribes from event bus
    /// </summary>
    private void OnDisable()
    {
        _normalModel.SetActive(false);
        _bounceModel.SetActive(false);
        _stickyModel.SetActive(false);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetNormal);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetSticky);
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
