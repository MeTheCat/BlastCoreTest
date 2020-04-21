using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class EnemyDeathEvent : UnityEvent<EnemyType> { }
[System.Serializable]
public class EnemySpawnEvent : UnityEvent<EnemyType> { }

[RequireComponent(typeof(Explosion))]
public class EnemyBall : MonoBehaviour, ITriggerProximityExplosion, IDieFromProximityExplosion<EnemyType>
{
    #region Private Fields
    [SerializeField]
    EnemyType type;

    Explosion explosion;

    [Tooltip("Can be killed while in motion/mid air or only when settled(asleep")]
    [SerializeField]
    private bool isCanDieInMotion = false;

    private bool isDying;

    [SerializeField]
    private GameObject deathPrefab;
    #endregion

    public EnemyDeathEvent EnemyDeathEvent = new EnemyDeathEvent();

    public EnemyDeathEvent EnemySpawnEvent = new EnemyDeathEvent();

    public void TriggerChainExplosion()
    {
        if (!isDying)
        {
            isDying = true;

            explosion.NotifyNeighbors(this.type);

            Die();
        }
    }

    public void OnProximityExplosion(EnemyType enemyType)
    {
        if (enemyType == this.type)
            TriggerChainExplosion();

        if (enemyType == EnemyType.BlastCore)
            Die();
    }

    public void OnTapped()
    {
        //if (!isCanDieInMotion && !rigidb.IsSleeping()) return;
        TriggerChainExplosion();
    }

    private void Die()
    {
        EnemyDeathEvent.Invoke(type);
        Destroy(gameObject);
        if (deathPrefab != null) Instantiate(deathPrefab, transform.position, transform.rotation);
    }

    #region Setup
    void Awake()
    {
        explosion = GetComponent<Explosion>();

        if (LevelManager.Instance != null)
        {
            this.EnemyDeathEvent.AddListener(LevelManager.Instance.OnEnemyDied);
            this.EnemySpawnEvent.AddListener(LevelManager.Instance.OnEnemySpawned);
        }

        EnemySpawnEvent.Invoke(type);
    }
    #endregion
}
