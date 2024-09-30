using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.transform.GetComponent<PlayerControl>().SetIfInPlanet(true, transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            other.transform.GetComponent<PlayerControl>().SetIfInPlanet(false);
        }
    }
}
