using UnityEngine;

public class ParticleSystemRadiusOverTime : MonoBehaviour
{
    private ParticleSystem.ShapeModule particleSystemShape;

    [SerializeField]
    private float addValue;

    [SerializeField]
    private float maxGrow;

    private void FixedUpdate()
    {
        if (particleSystemShape.enabled && particleSystemShape.radius < maxGrow)
        {
            particleSystemShape.radius += addValue;
        }
    }

    private void Awake()
    {
        particleSystemShape = GetComponent<ParticleSystem>().shape;
    }
}
