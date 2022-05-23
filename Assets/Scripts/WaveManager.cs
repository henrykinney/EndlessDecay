using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner Spawner;
    public int WaveMinute;
    public float WaveTime;
    public List<Wave> Waves;

    float EnemySpawnTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    void SpawnRandomWaveEnemy() {
        if (WaveMinute >= Waves.Count) {
            return; // THIS NEEDS TO DO SOMETHING COOL
        }
        Wave currentwave = Waves[WaveMinute];
        WaveTime += Time.fixedDeltaTime;
        if (WaveTime >= 60) {
            WaveTime -= 60;
            WaveMinute += 1;
        }
        EnemySpawnTime += currentwave.SpawnRate * Time.deltaTime;
        if (EnemySpawnTime >= 1) {
            Spawner.SpawnEnemy(currentwave.GetRandomSpawnEnemy(), currentwave.HealthMult);
            EnemySpawnTime -= 1;
        }
    }
    void FixedUpdate()
    {
        SpawnRandomWaveEnemy();
    }
}
