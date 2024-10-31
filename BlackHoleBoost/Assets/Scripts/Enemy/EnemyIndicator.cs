using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicatorPrefab;

    private GameObject _indicator;
    private float _offset=0.5f;

    // Update is called once per frame
    void Update()
    {
        if (IsOutOfBoundary())
        {
            if (_indicator==null)
            {
                _indicator = Instantiate(_indicatorPrefab);
            }
            _indicator.gameObject.SetActive(true);
            Vector3 indicatorPos = _indicator.transform.position;
            if (transform.position.y>GameManager.Instance.TopBoundary)
            {
                indicatorPos.x =Mathf.Clamp(transform.position.x,-GameManager.Instance.RightBoundary+_offset,GameManager.Instance.RightBoundary-_offset);
                indicatorPos.y = GameManager.Instance.TopBoundary - _offset;
            }
            else if (transform.position.y < -GameManager.Instance.TopBoundary)
            {
                indicatorPos.x = Mathf.Clamp(transform.position.x, -GameManager.Instance.RightBoundary+_offset, GameManager.Instance.RightBoundary-_offset);
                indicatorPos.y = -GameManager.Instance.TopBoundary + _offset;
            }
             if (transform.position.x>GameManager.Instance.RightBoundary)
            {
                indicatorPos.y = Mathf.Clamp(transform.position.y, -GameManager.Instance.TopBoundary+_offset, GameManager.Instance.TopBoundary-_offset);
                indicatorPos.x = GameManager.Instance.RightBoundary - _offset;
            }
            else if (transform.position.x < -GameManager.Instance.RightBoundary)
            {
                indicatorPos.y = Mathf.Clamp(transform.position.y, -GameManager.Instance.TopBoundary+_offset, GameManager.Instance.TopBoundary-_offset);
                indicatorPos.x = -GameManager.Instance.RightBoundary + _offset;
            }
            _indicator.transform.position = indicatorPos;
            _indicator.transform.rotation = Quaternion.LookRotation(_indicator.transform.forward, transform.position - _indicator.transform.position);
        }
        else
        {
            if (_indicator!=null)
            {
                _indicator.gameObject.SetActive(false);
            }
        }
    }

    private bool IsOutOfBoundary()
    {
        if (transform.position.x>GameManager.Instance.RightBoundary||transform.position.x<-GameManager.Instance.RightBoundary||transform.position.y>GameManager.Instance.TopBoundary||transform.position.y<-GameManager.Instance.TopBoundary)
        {
            return true;
        }
        return false;
    }

    private void OnDisable()
    {
        _indicator.gameObject.SetActive(false);
    }
}
