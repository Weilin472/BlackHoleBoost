using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [09/28/2024]
 * [sets damage and side effects of asteroid]
 */

public class ShootSmallAsteroidDamage : BaseDamageScript
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;
    private AsteroidMove _asteroidMove;

    delegate void AsteroidEffect(Collider collider);
    private AsteroidEffect asteroidEffect;

    [SerializeField] private int _normalDamage = 1;
    [SerializeField] private int _bunceDamage = 1;
    [SerializeField] private int _stickyDamage = 0;

    private List<GameObject> _stuckAsteroids;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
        _asteroidMove = GetComponent<AsteroidMove>();
        _stuckAsteroids = new List<GameObject>();
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
        if (_stuckAsteroids.Count > 0)
        {
            foreach (GameObject stuckAsteroid in _stuckAsteroids)
            {
                Destroy(stuckAsteroid);
            }
        }
        _stuckAsteroids = new List<GameObject>();
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
        if (asteroidEffect != Stick || other.gameObject.tag != "Asteroid")
        {
            base.OnTriggerEnter(other);
        }
        if (asteroidEffect != null)
        {
            asteroidEffect(other);
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
    private void Bounce(Collider collider)
    {
        _asteroidMove.ChangeDirection(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
    }

    /// <summary>
    /// attaches enemy asteroid to this asteroid
    /// </summary>
    private void Stick(Collider collider)
    {
        GameObject otherRoot = collider.transform.root.gameObject;

        if (otherRoot.tag == "Asteroid")
        {
            GameObject otherModel = collider.gameObject;
            for (int i = 0; i < _stuckAsteroids.Count; i++)
            {
                if (otherModel == _stuckAsteroids[i])
                {
                    return;
                }
            }

            Vector3 closestPoint = collider.ClosestPoint(transform.position);
            GameObject stuckModel = Instantiate(otherModel, closestPoint, Quaternion.identity);
            stuckModel.transform.parent = this.transform;
            _stuckAsteroids.Add(stuckModel);

            if (otherRoot.GetComponent<BaseHealthScript>())
            {
                BaseHealthScript otherHealth = otherRoot.GetComponent<BaseHealthScript>();
                otherHealth.Damage(99);
            }
        }
        //Debug.Log("Stick");
    }
}
