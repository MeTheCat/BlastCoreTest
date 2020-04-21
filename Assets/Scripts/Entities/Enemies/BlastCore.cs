using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Explosion))]
public class BlastCore : MonoBehaviour, ITriggerProximityExplosion, IDieFromProximityExplosion<EnemyType>
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

    public void OnTapped()
    {
        TriggerChainExplosion();
    }

    private void Die()
    {
        Destroy(gameObject);
        if (explodePrefab != null) Instantiate(explodePrefab, transform.position, transform.rotation);
    }

    #region Setup
    private void Awake()
    {
        blastExplosion = GetComponent<Explosion>();
    }
    #endregion
}
