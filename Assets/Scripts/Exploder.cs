using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _force;
    private float _radius;
    private List<Rigidbody> _repelledObjects = new List<Rigidbody>();
    
    public void Init(float force, float radius)
    {
        _force = force;
        _radius = radius;
    }

    public void AddRepelled(Rigidbody repelled)
        => _repelledObjects.Add(repelled);
    
    public void Run()
    {
        if (_repelledObjects.Count == 0)
            TakeRepelled();
        
        foreach (var repelledObject in _repelledObjects)
        {
            repelledObject.AddExplosionForce(_force, transform.position, _radius);
        }
    }

    private void TakeRepelled()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var collider in overlappedColliders)
        {
            if (collider.attachedRigidbody != null)
            {
                _repelledObjects.Add(collider.attachedRigidbody);
            }
        }
    }
}
