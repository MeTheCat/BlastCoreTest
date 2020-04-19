using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using UniRx;
using System;
using System.Collections.Generic;
using System.Linq;

public class BlastCore : MonoBehaviour, IDeathEventReciever<EnemyType>
{
    [SerializeField]
    EnemyType type = EnemyType.BlastCore;

    [SerializeField]
    private Collider2DTriggerContacts colliderTriggerContacts;

    private Rigidbody2D rigidb;

    private bool isDying = false;

    private void Awake()
    {
        rigidb = GetComponent<Rigidbody2D>();

        //d=this.killRangeCollider.OnTriggerStay2DAsObservable().Subscribe(collision =>
        //{
        //    Debug.DrawLine(this.transform.position, collision.transform.position, Color.green);
        //});
    }

    void OnCollisionStay2D(Collision2D collision)
    {
    }

    void OnMouseDown()
    {
        NotifyNeighborsAndDie();
    }

    private void NotifyNeighborsAndDie()
    {
        if (!isDying)
        {
            isDying = true;

            NotifyNeighbors();

            Die();
        }
    }

    private void NotifyNeighbors()
    {
        foreach (var contact in colliderTriggerContacts.GetContacts())
        {
            if (contact.gameObject.TryGetComponent(out IDeathEventReciever<EnemyType> reciever))
            {
                reciever.OnBlastCoreExplosionEvent();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rigidb.IsSleeping()) this.rigidb.WakeUp();
    }

    public void OnProximityDeathEvent(EnemyType type)
    {
    }

    //Can one blast core kill another?
    public void OnBlastCoreExplosionEvent()
    {
        Destroy(gameObject);
    }
}
