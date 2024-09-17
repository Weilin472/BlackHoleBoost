using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [9/17/2024]
 * [Any gameobject with this script, will wrap around the screen]
 */

[RequireComponent(typeof(Rigidbody))]
public class ScreenWarp : MonoBehaviour
{
    //game object's rigid body
    private Rigidbody _rigidbody;

    /// <summary>
    /// get needed components
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// gets the position of objects
    /// gets positions of screen corners
    /// uses those positions to see if the player is moving out of screen
    /// if true, then move position to other screen
    /// </summary>
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        if (screenPos.x <= 0 && _rigidbody.velocity.x < 0)
        {
            transform.position = new Vector3(topRight.x, transform.position.y, 0f);
        }
        else if (screenPos.x >= Screen.width && _rigidbody.velocity.x > 0)
        {
            transform.position = new Vector3(bottomLeft.x, transform.position.y, 0f);
        }
        if (screenPos.y >= Screen.height && _rigidbody.velocity.y > 0)
        {
            transform.position = new Vector3(transform.position.x, bottomLeft.y, 0f);
        }
        else if (screenPos.y <= 0 && _rigidbody.velocity.y < 0)
        {
            transform.position = new Vector3(transform.position.x, topRight.y, 0f);
        }
    }
}
