using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

/// <summary>
/// Collider2D behaving as a trigger doesn't keep track of it's contacts,
/// Use this instead.
/// </summary>
public class Collider2DTriggerContacts : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> contacts = new List<GameObject>();

    public List<GameObject> GetContacts()
    {
        return contacts.ToList();
    }

    private void OnDisable()
    {
        contacts.Clear();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        contacts.Add(collider.gameObject);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        contacts.Remove(collider.gameObject);
    }
}
