using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    [SerializeField]
    private bool isEnabled;

    [Tooltip("Camera will not pan beyound these bounds")]
    [SerializeField]
    private PolygonCollider2D aimBounds;

    [SerializeField]
    private float zoomSpeed = 0.1f;

    private Camera mainCamera;

    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    private bool isZooming;

    private bool IsPosWithinBounds(Vector3 position)
    {
        if (aimBounds.bounds.Contains(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isZooming)
                touchStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * zoomSpeed);

            isZooming = true;
        }
        else if (Input.GetMouseButton(0) && !isZooming)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsPosWithinBounds(mainCamera.transform.position + direction))
            {
                mainCamera.transform.position += direction;
            }
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.touchCount == 0)
            isZooming = false;
    }

    void Zoom(float increment)
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    void Start()
    {
        mainCamera = Camera.main;
    }
}
