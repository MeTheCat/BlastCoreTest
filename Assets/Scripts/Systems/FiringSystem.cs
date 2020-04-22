using UnityEngine;

public class FiringSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject laserBeam;

    public void FireLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 500.0f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out ILaserBeamTarget reciever))
            {
                reciever.ShotByLaserBeam();
            }
        }

        laserBeam?.SetActive(true);
    }

    #region Setup
    void Awake()
    {

    }
    #endregion
}
