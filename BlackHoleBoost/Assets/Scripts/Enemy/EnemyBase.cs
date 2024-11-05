using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected float _currentSpeed;
    protected Rigidbody _rigid;

    protected EnemyHealthScript _enemyHealthScript;

    protected bool isStuck = false;

    public PlayerControl targetPlayer;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigid = transform.GetComponent<Rigidbody>();
        _enemyHealthScript = GetComponent<EnemyHealthScript>();
        targetPlayer = GameManager.Instance.GetPlayerWithMoreHealth();
        if (targetPlayer==null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerControl>();
        }
    }


    protected virtual void FixedUpdate()
    {
        if (isStuck)
        {
            _currentSpeed = 0;
        }
        else
        {
            _currentSpeed = _speed;
        }
        Movement();
    }

    protected virtual void Movement()
    {
        if (GameManager.Instance._inPrototype&& GameManager.Instance.players.Count > 0)
        {    
                targetPlayer = GameManager.Instance.GetPlayerWithMoreHealth();          
        }
        else
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerControl>();
        }
        Vector3 playerPos = targetPlayer.transform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        _rigid.velocity = dir * _currentSpeed;
    }
    protected bool DetectBoundaries()
    {
        if ((transform.position.x > GameManager.Instance.RightBoundary) || (transform.position.x < -GameManager.Instance.RightBoundary) || (transform.position.y > GameManager.Instance.TopBoundary) || (transform.position.y < -GameManager.Instance.TopBoundary))
        {
            Vector3 currPos = transform.position;
            if (transform.position.x>GameManager.Instance.RightBoundary)
            {
                currPos.x -= 0.5f;
            }
            else if (transform.position.x <-GameManager.Instance.RightBoundary)
            {
                currPos.x += 0.5f;
            }
            if (transform.position.y>GameManager.Instance.TopBoundary)
            {
                currPos.y -= 0.5f;
            }
            else if (transform.position.y<-GameManager.Instance.TopBoundary)
            {
                currPos.y += 0.5f;
            }
            transform.position = currPos;
            return true;
        }
        return false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthScript otherHealth = other.transform.root.gameObject.GetComponent<PlayerHealthScript>();
            otherHealth.Damage(1);
            if (PlaytestDataCollector.Instance != null)
            {
                if (GetComponent<Cyclop>())
                {
                    PlaytestDataCollector.Instance.AddPlayerHit("Cyclops");
                }
                else if (GetComponent<Minotaur>())
                {
                    PlaytestDataCollector.Instance.AddPlayerHit("Minotaur");
                }
            }
        }
    }

    public void GetStick()
    {
        isStuck = true;
    }

    public void Unstick()
    {
        isStuck = false;
    }

    private void OnDestroy()
    {
        if (EnemySpawner.Instance!=null)
        {
            EnemySpawner.Instance.EnemyDestroy(this);
        }
        
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
}
