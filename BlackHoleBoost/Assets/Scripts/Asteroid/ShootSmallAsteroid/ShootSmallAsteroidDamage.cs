using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/16/2024]
 * [sets damage and side effects of asteroid]
 */

public class ShootSmallAsteroidDamage : BaseDamageScript
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;
    private AsteroidMove _asteroidMove;

    delegate void AsteroidEffect();
    private AsteroidEffect asteroidEffect;

    [SerializeField] private int _normalDamage = 1;
    [SerializeField] private int _bunceDamage = 1;
    [SerializeField] private int _stickyDamage = 0;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
        _asteroidMove = GetComponent<AsteroidMove>();
    }

    /// <summary>
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
        asteroidEffect = null;
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.NORMAL, SetNormal);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.BOUNCE, SetBounce);
        _shootSmallAsteroidEventBus.Unsubscribe(SmallAsteroidType.STICKY, SetSticky);
    }

    /// <summary>
    /// when asteroid collides with other collider: deal damage, and if needed
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (asteroidEffect != null)
        {
            asteroidEffect();
        }
    }

    /// <summary>
    /// sets damage to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _damage = _normalDamage;
        asteroidEffect = null;
    }

    /// <summary>
    /// sets damage to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _damage = _bunceDamage;
        asteroidEffect += Bounce;
    }

    /// <summary>
    /// sets damage to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _damage = _stickyDamage;
        asteroidEffect += Stick;
    }

    /// <summary>
    /// changes rotation of game object to bounce
    /// </summary>
    private void Bounce()
    {
        _asteroidMove.ChangeDirection(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
        Debug.Log("Bounce");
    }

    /// <summary>
    /// attaches enemy asteroid to this asteroid
    /// </summary>
    private void Stick()
    {
        Debug.Log("Stick");
    }
}
