using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Keeps track of the level state
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public UnityEvent LevelClearedEvent = new UnityEvent();

    public UnityEvent EnemyKilledEvent = new UnityEvent();

    #region PrivateFields
    [SerializeField]
    private int levelClearedAtEnemiesLeft;

    [SerializeField]
    private int enemiesCount = 0;

    [SerializeField]
    private int blastCoreCount = 0;
    #endregion

    public int GetTotalEnemiesCount()
    {
        return enemiesCount;
    }

    public int GetBlastCoreCount()
    {
        return blastCoreCount;
    }

    public void OnEnemySpawned(EnemyType enemyType)
    {
        enemiesCount++;
    }

    public void OnEnemyDied(EnemyType enemyType)
    {
        enemiesCount--;

        EnemyKilledEvent.Invoke();

        if (enemiesCount < levelClearedAtEnemiesLeft)
        {
            LevelClearedEvent.Invoke();
        }
    }

    #region Setup
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
