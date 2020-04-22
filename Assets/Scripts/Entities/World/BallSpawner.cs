using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (spawerState == SpawnerState.Spawning) return;

        spawerState = SpawnerState.Spawning;

        Observable.FromCoroutine(KeepSpawning).Subscribe(_ => spawerState = SpawnerState.Idle);
    }

    IEnumerator KeepSpawning()
    {
        foreach (var enemyType in enemiesToSpawn.Select((value, index) => new { value, index }))
        {
            for (int e = 0; e < enemyType.value.NumObjects; e++)
            {
                SpawnAtLocation(enemyType.value.Prefab, spawnPoints.RandomElement());

                yield return new WaitForSeconds(spawnDelayMs / 1000);
            }
        }
    }

    private void SpawnAtLocation(GameObject prefab, Transform location)
    {
        Vector3 deviateByX = new Vector3(Random.Range(-randXDeviation, randXDeviation), 0, 0);

        Instantiate(prefab, location.transform.position + deviateByX, location.transform.rotation);
    }

    #region Setup
    void Start()
    {
        SpawnEnemies();
    }
    #endregion
}
