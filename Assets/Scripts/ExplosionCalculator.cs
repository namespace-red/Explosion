using UnityEngine;

public class ExplosionCalculator
{
    private static Vector3 _startScale;
    private static float _startExplosionForce;
    private static float _startExplosionRadius;

    public ExplosionCalculator(Vector3 startScale, float startExplosionForce, float startExplosionRadius)
    {
        _startScale = startScale;
        _startExplosionForce = startExplosionForce;
        _startExplosionRadius = startExplosionRadius;
    }

    public float GetForce(Vector3 scale)
        => GetInverseValueByScale(scale, _startExplosionForce);

    public float GetRadius(Vector3 scale)
        => GetInverseValueByScale(scale, _startExplosionRadius);

    private float GetInverseValueByScale(Vector3 scale, float value)
    {
        float proportion = 1 - scale.x / _startScale.x;
        return proportion * value + value;
    }
}
