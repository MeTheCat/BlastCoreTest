using UnityEngine;

/// <summary>
/// Allows to notify objects in proximity of the explosion
/// </summary>
[RequireComponent(typeof(Collider2DTriggerContacts))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Collider2DTriggerContacts collider2DTriggerContacts;

    public void NotifyNeighbors<T>(T entityType)
    {
        foreach (var contact in collider2DTriggerContacts.GetContacts())
        {
            if (contact.gameObject.TryGetComponent(out IDieFromProximityExplosion<T> reciever))
            {
                reciever.OnProximityExplosion(entityType);
            }
        }
    }
}