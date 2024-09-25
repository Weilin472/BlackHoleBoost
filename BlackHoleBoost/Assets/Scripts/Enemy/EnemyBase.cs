using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _rigid;

    protected EnemyHealthScript _enemyHealthScript;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigid = transform.GetComponent<Rigidbody>();
        _enemyHealthScript = GetComponent<EnemyHealthScript>();
    }


    protected virtual void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (PlayerControl.Instance != null)
        {
            Vector3 playerPos = PlayerControl.Instance.transform.position;
            Vector3 dir = (playerPos - transform.position).normalized;
            _rigid.velocity = dir * _speed;
        }
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
            _rigid.velocity = Vector3.zero;
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
        }
    }
}
