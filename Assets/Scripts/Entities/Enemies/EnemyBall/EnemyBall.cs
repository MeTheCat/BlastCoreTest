using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EnemyBall : MonoBehaviour, IDeathEventReciever<EnemyType>
{
    #region Private Fields
    [SerializeField]
    EnemyType type;

    //[Tooltip("Collider that determines kill range")]
    //[SerializeField]
    private Collider2D collider;

    [Tooltip("Can be killed while in motion/mid air or only when settled(asleep")]
    [SerializeField]
    private bool isCanDieInMotion = false;

    private Rigidbody2D rigidb;

    ContactFilter2D filter = new ContactFilter2D();

    private bool isDying;
    #endregion

    public void Destroy()
    {
        //Turn on effect/animation
        Destroy(gameObject);
    }

    void Update()
    {
        //if (rigidb.IsSleeping())
        //{
        //    rigidb.WakeUp();
        //}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("On trigger stay");
        //Debug.Log(other.gameObject);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.DrawLine(this.transform.position, collision.transform.position, Color.green);
    }

    public void OnProximityDeathEvent(EnemyType enemyType)
    {
        if (enemyType == this.type)
        {
            NotifyNeighborsAndDie();
        }
    }

    public void OnBlastCoreExplosionEvent()
    {
        Die();
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

    //TODO: Check if it works for mobile, not sure if it does
    void OnMouseDown()
    {
        //if (!isCanDieInMotion && !rigidb.IsSleeping()) return;

        //TODO: Check if I'm eligible to be destroyed

        NotifyNeighborsAndDie();
    }

    private void NotifyNeighbors()
    {
        List<Collider2D> contacts = new List<Collider2D>();

        collider.GetContacts(filter, contacts);

        foreach (var contact in contacts)
        {
            if (contact.gameObject.TryGetComponent(out IDeathEventReciever<EnemyType> reciever))
            {
                reciever.OnProximityDeathEvent(this.type);
            }
        }
    }

    private void Die()
    {
        Destroy();
    }

    void Awake()
    {
        filter.useTriggers = true;

        collider = GetComponent<Collider2D>();

        rigidb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //neighborDiedEvent.AddListener(OnNeighborDiedEvent);
    }
}
