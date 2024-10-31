using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxExpose : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject exposureEffect;
    [SerializeField] private int damage;
    [SerializeField] private float effectTime;
    public Vector3 TargetPos;
    private List<PlayerControl> AffectedPlayers = new List<PlayerControl>();

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,TargetPos)<=0.4f)
        {
            exposureEffect.gameObject.SetActive(true);
            for (int i = 0; i < AffectedPlayers.Count; i++)
            {
                AffectedPlayers[i].HitBySphinx(damage, effectTime);
            }
            Invoke("DestroySelf", 0.5f);
        }
        else
        {
            Vector3 dir = (TargetPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(transform.forward, dir);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed*Time.deltaTime);
        }

    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AffectedPlayers.Add(other.gameObject.transform.GetComponent<PlayerControl>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AffectedPlayers.Remove(other.gameObject.transform.GetComponent<PlayerControl>());
        }
    }
}
