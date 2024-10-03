using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/03/2024]
 * [sets damage and side effects of asteroid]
 */

public class ShootSmallAsteroidDamage : BaseDamageScript
{
    private ShootSmallAsteroidEventBus _shootSmallAsteroidEventBus;
    private AsteroidMove _asteroidMove;

    delegate void AsteroidEffect(Collider collider);
    private AsteroidEffect _asteroidEffect;

    [SerializeField] private int _normalDamage = 1;
    [SerializeField] private int _bunceDamage = 1;
    [SerializeField] private int _stickyDamage = 0;

    private List<GameObject> _stuckAsteroids;
    private List<EnemyBase> _stuckEnemies;
    private List<Vector3> _enemyOffset;

    /// <summary>
    /// gets needed components
    /// </summary>
    private void Awake()
    {
        _shootSmallAsteroidEventBus = GetComponent<ShootSmallAsteroidEventBus>();
        _asteroidMove = GetComponent<AsteroidMove>();
        _stuckAsteroids = new List<GameObject>();
        _stuckEnemies = new List<EnemyBase>();
        _enemyOffset = new List<Vector3>();
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
        _asteroidEffect = null;
        if (_stuckAsteroids.Count > 0)
        {
            foreach (GameObject stuckAsteroid in _stuckAsteroids)
            {
                Destroy(stuckAsteroid);
            }
        }
        if (_stuckEnemies.Count > 0)
        {
            for (int i = 0; i < _stuckEnemies.Count; i++)
            {
                _stuckEnemies[i].Unstick();
                if (_stuckEnemies[i].gameObject.GetComponent<Cyclop>())
                {
                    Cyclop cyclop = _stuckEnemies[i].gameObject.GetComponent<Cyclop>();
                    cyclop.TurnOnLaser();
                }
            }
            _stuckEnemies = new List<EnemyBase>();
            _enemyOffset = new List<Vector3>();
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
        if (_asteroidEffect != Stick || other.gameObject.tag != "Asteroid")
        {
            base.OnTriggerEnter(other);
        }
        if (_asteroidEffect != null)
        {
            _asteroidEffect(other);
        }
    }

    /// <summary>
    /// sets damage to normal asteroid
    /// </summary>
    private void SetNormal()
    {
        _damage = _normalDamage;
        _asteroidEffect = null;
    }

    /// <summary>
    /// sets damage to bounce asteroid
    /// </summary>
    private void SetBounce()
    {
        _damage = _bunceDamage;
        _asteroidEffect += Bounce;
    }

    /// <summary>
    /// sets damage to sticky asteroid
    /// </summary>
    private void SetSticky()
    {
        _damage = _stickyDamage;
        _asteroidEffect += Stick;
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

        //if asteroid: add the model to this asteroid as a child
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

            //gets playtest data
            if (PlaytestData.Instance != null)
            {
                PlaytestData.Instance.asteroidsStuck++;
            }

            if (otherRoot.GetComponent<BaseHealthScript>())
            {
                BaseHealthScript otherHealth = otherRoot.GetComponent<BaseHealthScript>();
                otherHealth.Damage(99);
            }
        }

        //if enemy, set enmey script to stuck
        if (otherRoot.GetComponent<EnemyBase>())
        {
            EnemyBase otherEnemy = otherRoot.GetComponent<EnemyBase>();

            otherEnemy.GetStick();
            _stuckEnemies.Add(otherEnemy);
            _enemyOffset.Add(otherEnemy.transform.position - this.transform.position);
            //gets playtest data
            if (PlaytestData.Instance != null)
            {
                PlaytestData.Instance.enemiesStuck++;
            }

            if (otherRoot.GetComponent<Cyclop>())
            {
                Cyclop cyclop = otherRoot.GetComponent<Cyclop>();
                cyclop.TurnOffLaser();
            }
        }
        //Debug.Log("Stick");
    }

    private void FixedUpdate()
    {
        if (_asteroidEffect == Stick)
        {
            if (_stuckEnemies.Count > 0)
            {
                for (int i = _stuckEnemies.Count -1 ; i >= 0; i--)
                {
                    if (_stuckEnemies[i] != null)
                    {
                        _stuckEnemies[i].transform.position = transform.position + _enemyOffset[i];
                    }
                    else
                    {
                        _stuckEnemies.RemoveAt(i);
                        _enemyOffset.RemoveAt(i);
                    }
                }
                
            }
        }
    }
}
