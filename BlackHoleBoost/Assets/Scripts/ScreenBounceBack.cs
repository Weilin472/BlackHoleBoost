using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounceBack : MonoBehaviour
{
    private float _rightBoundary;
    private float _topBoundary;
    [SerializeField] private float _boundBackDistance;

    // Start is called before the first frame update
    void Start()
    {
        _rightBoundary = Camera.main.orthographicSize * Screen.width / Screen.height;
        _topBoundary = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _rightBoundary)
        {
            float newX = transform.position.x - _boundBackDistance;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            Vector3 vel = transform.GetComponent<Rigidbody>().velocity;
            if (vel.x>0)
            {
                vel.x *= -1;
                transform.GetComponent<Rigidbody>().velocity = vel;
            }
            //  transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (transform.position.x < -_rightBoundary)
        {
            float newX = transform.position.x + _boundBackDistance;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            Vector3 vel = transform.GetComponent<Rigidbody>().velocity;
            if (vel.x < 0)
            {
                vel.x *= -1;
                transform.GetComponent<Rigidbody>().velocity = vel;
            }
            //     transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
        else if (transform.position.y > _topBoundary+0.5)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            Vector3 vel = transform.GetComponent<Rigidbody>().velocity;
            if (vel.y>0)
            {
                vel.y *= -1;
                transform.GetComponent<Rigidbody>().velocity = vel;
            }
        }
        else if (transform.position.y < -_topBoundary + 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 vel = transform.GetComponent<Rigidbody>().velocity;
            if (vel.y < 0)
            {
                vel.y *= -1;
                transform.GetComponent<Rigidbody>().velocity = vel;
            }
        }
    }
}