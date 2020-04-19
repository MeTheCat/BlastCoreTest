using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class BlastCore : MonoBehaviour, ITriggerProximityExplosion, IDieFromProximityExplosion<EnemyType>
{
    #region Private fields
    [SerializeField]
    EnemyType type = EnemyType.BlastCore;

    Explosion blastExplosion;

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
    }

    void OnMouseDown()
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
