using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float EnemySpawnSpeed;
    public float EnemyHealthMult;
    public GameObject PrefabToSpawn;
    float EnemySpawnTime;
    public float SpawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawnTime += EnemySpawnSpeed * Time.deltaTime;
        if (EnemySpawnTime >= 1) {
            SpawnEnemy();
            EnemySpawnTime -= 1;
        }
    }
    void SpawnEnemy()
    {
        GameObject newthing = Instantiate(PrefabToSpawn);
        float angle = Random.Range(0, Mathf.PI * 2);
        
        newthing.transform.position = new Vector3(Mathf.Cos(angle) * SpawnRadius, Mathf.Sin(angle) * SpawnRadius, 0) + gameObject.transform.position;
        newthing.GetComponent<Health>().MaxHealth *= EnemyHealthMult;
    }
}
