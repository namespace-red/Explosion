using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private static float _force;
    private static float _radius;
    
    public void Init(float force, float radius)
    {
        _force = force;
        _radius = radius;
    }
    
    public void Run()
    {
        Vector3 position = transform.position;
        
        var explodableObjects = GetExplodables(position, _radius);
        
        foreach (var explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(_force, position, _radius);
        }
    }

    private List<Rigidbody> GetExplodables(Vector3 position, float radius)
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(position, radius);
        var rigidbodies = new List<Rigidbody>();

        foreach (var collider in overlappedColliders)
        {
            if (collider.attachedRigidbody != null)
            {
                rigidbodies.Add(collider.attachedRigidbody);
            }
        }

        return rigidbodies;
    }
}
