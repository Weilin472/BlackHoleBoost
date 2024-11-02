using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphinx : EnemyBase
{
    private bool isShooting;
    private bool readyToShoot;
    [SerializeField] private float chargeTime;
    private float currentChargeTime;
    [SerializeField] private GameObject sphinxExposure;
     private int bulletLeft;
    [SerializeField] private int maxBullet;
    private bool powerDown;
    [SerializeField] private float powerdownTime;
    private float currentPowerDownTime;

    protected override void Start()
    {
        base.Start();
        bulletLeft = maxBullet;
    }
    protected override void Movement()
    {
       
    }

    protected override void Update()
    {
        base.Update();
        if (!powerDown)
        {
            if (!readyToShoot)
            {
                Charging();
            }

            if (!isShooting)
            {
                Aiming();
            }
        }
        else
        {
            PowerDown();
        }
       
    }

    private void StartShooting()
    {
        if (readyToShoot)
        {
            Instantiate(sphinxExposure, transform.position, transform.rotation).GetComponent<SphinxExpose>().TargetPos=targetPlayer.transform.position;
            readyToShoot = false;
            bulletLeft--;
            if (bulletLeft<=0)
            {
                powerDown = true;
                currentPowerDownTime = 0;
                readyToShoot = false;
                transform.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
        isShooting = false;
    }

    private void Charging()
    {
        currentChargeTime += Time.deltaTime;
        if (currentChargeTime >= chargeTime)
        {
            readyToShoot = true;
            currentChargeTime = 0;
        }
    }

    private void PowerDown()
    {
        currentPowerDownTime += Time.deltaTime;
        if (currentPowerDownTime >= powerdownTime)
        {
            powerDown = false;
            currentPowerDownTime = 0;
            bulletLeft = maxBullet;
            currentChargeTime = 0;
            readyToShoot = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.gray;
        }
    }

    private void Aiming()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.up));
        RaycastHit[] hits = Physics.RaycastAll(ray, 100);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag == "Player")
            {
                isShooting = true;
                Invoke("StartShooting", 0.5f);
                break;
            }
        }
        if (!isShooting)
        {
            transform.Rotate(new Vector3(0, 0, -_speed) * Time.deltaTime);
        }
    }
}
