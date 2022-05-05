using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRamper : MonoBehaviour
{
    EnemySpawner spawner;
    public float SpawnSpeedRamp;
    public float HealthMultRamp;
    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawner.EnemySpawnSpeed += SpawnSpeedRamp * Time.fixedDeltaTime;
        spawner.EnemyHealthMult += HealthMultRamp * Time.fixedDeltaTime;
    }
}
