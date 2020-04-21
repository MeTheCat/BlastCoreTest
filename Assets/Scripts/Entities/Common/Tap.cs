using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tap : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    UnityEvent OnTappedOnce = new UnityEvent();

    [SerializeField]
    float dragThreshold = 5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        float dragDistance = Vector2.Distance(eventData.pressPosition, eventData.position);

        bool isDrag = dragDistance > dragThreshold;

        if (isDrag)
            return;

        OnTappedOnce.Invoke();
    }
}
