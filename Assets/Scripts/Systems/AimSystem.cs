using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    #region Private fields
    [SerializeField]
    private bool isEnabled = false;

    [Tooltip("Camera will not pan beyound these bounds")]
    [SerializeField]
    private PolygonCollider2D aimBounds;

    [SerializeField]
    private float zoomSpeed = 0.1f;

    [SerializeField]
    private GameObject aimHUD;

    [SerializeField]
    private float zoomOutMin = 1;
    [SerializeField]
    private float zoomOutMax = 8;

    private Camera mainCamera;

    private Vector3 originalPosition;

    private bool isZooming;
    private Vector3 touchStart;
    #endregion

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

    public void ToggleAimActive()
    {
        isEnabled = !isEnabled;
        aimHUD.SetActive(isEnabled);

        //TODO: Dottween smoothly to the original position
        if (!isEnabled)
        {
            mainCamera.orthographicSize = this.zoomOutMax;
            mainCamera.transform.position = originalPosition;
        } 
    }

    void Update()
    {
        if (!isEnabled) return;

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

    #region Setup
    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;
    }
    #endregion
}
