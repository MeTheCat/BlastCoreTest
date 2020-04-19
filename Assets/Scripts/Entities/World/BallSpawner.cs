using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO: Optimizie with object pool
/// <summary>
/// Spawn enemies/balls at spawn points with the definied time interval
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyTypeNumber
    {
        public int NumObjects;
        public GameObject Prefab;
    }

    public enum SpawnerState
    {
        Idle,
        Spawning
    }

    #region EditorValues
    [SerializeField]
    private List<EnemyTypeNumber> enemiesToSpawn = null;

    [SerializeField]
    private int spawnDelayMs = 300;

    [SerializeField]
    private float randXDeviation = 0.02f;

    [SerializeField]
    private List<Transform> spawnPoints;
    #endregion

    private SpawnerState spawerState = SpawnerState.Idle;

    public void SpawnEnemies()
    {
        foreach (var enemyType in enemiesToSpawn.Select((value, index) => new { value, index }))
        {
            for (int e = 0; e < enemyType.value.NumObjects; e++)
            {
                SpawnDelayedAtLocation(enemyType.value.Prefab, spawnPoints.RandomElement(), spawnDelayMs * (enemyType.index + 1) * (e + 1));
            }
        }

        // SpawnDelayedAtLocation(objectsToSpawn[0].Prefab, spawnPoints[0], spawnDelayMs);
    }

    /// <summary>
    /// Spawn immedietly or queue spawning
    /// </summary>
    public void SpawnMoreOrQueue()
    {

    }

    public void SpawnMore(List<EnemyTypeNumber> enemies)
    {
        if (spawerState.Equals(SpawnerState.Spawning))
        {
            Debug.LogError("Busy spawning, queuing instead");
            return;
        }
    }

    private void SpawnDelayedAtLocation(GameObject prefab, Transform location, int delay)
    {
        Vector3 deviateByX = new Vector3(Random.Range(-randXDeviation, randXDeviation), 0, 0);
        Observable.Timer(TimeSpan.FromMilliseconds(delay)).Subscribe(
                    x => Instantiate(prefab, location.transform.position + deviateByX, location.transform.rotation));
    }

    private void SpawnAtLocation(GameObject prefab, Transform location)
    {
        Instantiate(prefab, location.transform.position, location.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }
}
