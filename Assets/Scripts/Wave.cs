using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public float SpawnRate;
    public float HealthMult;
    public List<GameObject> BossEnemies;
    public List<GameObject> RegularEnemies;

    public GameObject GetRandomSpawnEnemy() {
        return RegularEnemies[Random.Range(0, RegularEnemies.Count)];
    }

}
