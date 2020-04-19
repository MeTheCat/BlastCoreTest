using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathEventReciever<T>
{
    void OnProximityDeathEvent(T type);

    void OnBlastCoreExplosionEvent();
}
