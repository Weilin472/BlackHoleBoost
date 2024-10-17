using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpOrbs : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        Invoke("DestroySelf", 3);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (!player.isInBlackHole&&!player.IsFreeze)
            {
                other.GetComponent<PlayerControl>().Freeze();
                CancelInvoke();
                Destroy(gameObject);
            }
           
        }
    }
}
