using UnityEngine;

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
    #endregion

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

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        //if (!isCanDieInMotion && !rigidb.IsSleeping()) return;

        TriggerChainExplosion();
    }

    #region Setup
    void Awake()
    {
        explosion = GetComponent<Explosion>();
    }

    void Start()
    {

    }
    #endregion
}
