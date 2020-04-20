using System.Diagnostics.Contracts;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    [SerializeField]
    private float tapTimeThreshold = 0.4f;

    private Camera mainCamera;

    private float[] touchStartTime = new float[10];
    private bool[] touchMoved = new bool[10];

    private void RaycastSingleTap(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(pos), Vector2.zero);

        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent(out ITappable reciever))
            {
                reciever.OnTappedOnce();
            }
        }
    }

    private void ProcessTouchForMobile()
    {
        foreach (Touch touch in Input.touches)
        {
            int fingerId = touch.fingerId;

            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime[fingerId] = Time.time;
                touchMoved[fingerId] = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchMoved[fingerId] = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                float tapTime = Time.time - touchStartTime[fingerId];
                if (tapTime <= tapTimeThreshold && touchMoved[fingerId] == false)
                {
                    RaycastSingleTap(touch.position);
                }
            }
        }
    }

    private void ProcessTouchForEditor()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStartTime[0] = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float tapTime = Time.time - touchStartTime[0];
            if (tapTime <= tapTimeThreshold && touchMoved[0] == false)
            {
                RaycastSingleTap(Input.mousePosition);
            }
        }
#endif
    }

    private void Update()
    {
        ProcessTouchForMobile();

        ProcessTouchForEditor();
    }

    #region Setup
    void Start()
    {
        mainCamera = Camera.main;
    }
    #endregion
}