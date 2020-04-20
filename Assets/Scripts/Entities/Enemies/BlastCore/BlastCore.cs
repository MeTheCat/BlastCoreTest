using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class BlastCore : MonoBehaviour, ITappable, ITriggerProximityExplosion, IDieFromProximityExplosion<EnemyType>
{
    #region Private fields
    [SerializeField]
    EnemyType type = EnemyType.BlastCore;

    Explosion blastExplosion;

    [SerializeField]
    private GameObject implodePrefab;
    [SerializeField]
    private GameObject explodePrefab;

    private bool isDying = false;
    #endregion

    public void TriggerChainExplosion()
    {
        if (!isDying)
        {
            isDying = true;

            blastExplosion.NotifyNeighbors(this.type);

            Die();
        }
    }

    public void OnProximityExplosion(EnemyType type)
    {

        if (type == EnemyType.BlastCore) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        if (explodePrefab != null) Instantiate(explodePrefab, transform.position, transform.rotation);
    }

    public void OnTappedOnce()
    {
        TriggerChainExplosion();
    }

    #region Setup
    private void Awake()
    {
        blastExplosion = GetComponent<Explosion>();
    }
    #endregion
}
