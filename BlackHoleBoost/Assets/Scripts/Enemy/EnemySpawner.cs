using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    private int phase = 0;
    private int loop = 1;
    private List<EnemyBase> currentEnemies = new List<EnemyBase>();

    private static EnemySpawner _instance;
    public static EnemySpawner Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance!=null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
    }

    private void Start()
    {
        Invoke("SpawnEnemy", 2);
    }

    private void SpawnEnemy()
    {
        currentEnemies.Clear();
        for (int i = 0; i < loop; i++)
        {
            int enemyIndex = 0;
            if (loop <= 3)
            {
                enemyIndex = phase % Enemies.Count;
            }
            else
            {
                enemyIndex = Random.Range(0, Enemies.Count);
            }
            float posX = Random.Range(-GameManager.Instance.RightBoundary, GameManager.Instance.RightBoundary);
            float posY = Random.Range(-GameManager.Instance.TopBoundary, GameManager.Instance.TopBoundary);
            Vector3 pos = new Vector3(posX, posY, 0);
            if (Enemies[enemyIndex].transform.GetComponent<Sphinx>())
            {
                pos = Camera.main.transform.position;
                pos.z = 0;
                float offset = GameManager.Instance.RightBoundary * 2 / 10;
                pos.x = i * offset-(loop-1)*(offset/2);
                pos.x = Mathf.Clamp(pos.x, -GameManager.Instance.RightBoundary, GameManager.Instance.RightBoundary);
            }
           currentEnemies.Add(Instantiate(Enemies[enemyIndex], pos, Quaternion.identity).transform.GetComponent<EnemyBase>());
        }
    }

    private void PhaseProgress()
    {
        phase++;
        loop = phase / Enemies.Count + 1;
        SpawnEnemy();
    }

    public void EnemyDestroy(EnemyBase e)
    {
        currentEnemies.Remove(e);
        if (currentEnemies.Count==0)
        {
            currentEnemies.Clear();
            Invoke("PhaseProgress", 2);
        }
    }

}
