using UnityEngine;

public class ExplosionCalculator
{
    private Vector3 _startScale;
    private float _startForce;
    private float _startRadius;

    public ExplosionCalculator(Vector3 startScale, float startExplosionForce, float startExplosionRadius)
    {
        _startScale = startScale;
        _startForce = startExplosionForce;
        _startRadius = startExplosionRadius;
    }

    public float GetForce(Vector3 scale)
        => GetInverseValueByScale(scale, _startForce);

    public float GetRadius(Vector3 scale)
        => GetInverseValueByScale(scale, _startRadius);

    private float GetInverseValueByScale(Vector3 scale, float value)
    {
        float proportion = 1 - scale.x / _startScale.x;
        return proportion * value + value;
    }
}
