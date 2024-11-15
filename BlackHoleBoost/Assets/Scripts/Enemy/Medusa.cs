using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : EnemyBase
{
    [SerializeField] private Transform gorgonParentTran;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _detectDistance;
    [SerializeField] private GameObject _empPrefab;
    [SerializeField] private float shootingCoolDown;

    private bool _isShooting = false;
    private bool _isLockingOntoPlayer;
    
 

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        gorgonParentTran.Rotate(0, 0, -_rotateSpeed * Time.deltaTime) ;
    }

    protected override void Movement()
    {
        if (!_isLockingOntoPlayer)
        {
            base.Movement();
            if (Vector3.Distance(targetPlayer.transform.position, transform.position)<=_detectDistance)
            {
                _isLockingOntoPlayer = true;
                targetPlayer.SetLockOnIcon(true);
                _rigid.velocity = Vector3.zero;
                if (!_isShooting)
                {
                    _isShooting = true;
                    StartCoroutine(ShootingAnimation());
                }
            }
        }   
        else if(Vector3.Distance(targetPlayer.transform.position,transform.position)>_detectDistance)
        {
            _isLockingOntoPlayer = false;
            targetPlayer.SetLockOnIcon(false);

        }

    }

    private IEnumerator ShootingAnimation()
    {
        GameObject gorgon = GetNearestGorgon(targetPlayer.gameObject);
        while (_isLockingOntoPlayer&&gorgon!=null)
        {
            GameObject emp = Instantiate(_empPrefab, gorgon.transform.position, Quaternion.identity);
            emp.transform.rotation = Quaternion.LookRotation(emp.transform.forward, targetPlayer.transform.position - emp.transform.position);
            yield return new WaitForSeconds(shootingCoolDown);
        }
        _isShooting = false;
    }

    /// <summary>
    /// get the gorgon that is the nearest to the input targetObject
    /// </summary>
    /// <param name="targetObject"></param>
    /// <returns></returns>
    public GameObject GetNearestGorgon(GameObject targetObject)
    {
        float nearestDistance = 9999;
        GameObject nearestGorgon = null;
        foreach (Transform child in gorgonParentTran)
        {
            float dis = Vector3.Distance(targetObject.transform.position, child.position);
            if (dis<nearestDistance)
            {
                nearestGorgon = child.gameObject;
                nearestDistance = dis;
            }
        }
        return nearestGorgon;
    }
}
