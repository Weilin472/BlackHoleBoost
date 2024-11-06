using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclop : EnemyBase
{
    [SerializeField] private Transform _laser;
    [SerializeField] private GameObject _laserObject;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _distanceToTurnOnLaser = 5f;


    protected override void Update()
    {
        base.Update();
        transform.Rotate(0, 0, -_rotateSpeed * Time.deltaTime);
        CheckLaser();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthScript otherHealth = other.transform.root.gameObject.GetComponent<PlayerHealthScript>();
            otherHealth.Damage(1);
        }
        if (other.tag == "BlackHole")
        {
            _enemyHealthScript.Damage(1);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BlackHole")
        {
            _enemyHealthScript.Damage(1);
        }
    }

    private void CheckLaser()
    {
        Vector3 playerPos = Vector3.zero;
        if (GameManager.Instance._inPrototype)
        {
            if (GameManager.Instance.players.Count > 0)
            {
                playerPos = GameManager.Instance.GetPlayerWithMoreHealth().transform.position;
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
            
            
        }

        float dist = Vector3.Distance(transform.position, playerPos);
        if (dist <= _distanceToTurnOnLaser)
        {
            TurnOnLaser();
        }
        else
        {
            TurnOffLaser();
        }
    }

    public void TurnOffLaser()
    {
        _laserObject.SetActive(false);
    }

    public void TurnOnLaser()
    {
        _laserObject.SetActive(true);
    }
}
