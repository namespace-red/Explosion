using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private Collider _spawnZone;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void Awake()
    {
        _cubePrefab = _cubePrefab ?? throw new NullReferenceException(nameof(_cubePrefab));
    }

    public Cube Create(Vector3 position, Vector3 scale, Color color, float splitChance)
    {
        Cube newCube = Instantiate(_cubePrefab, position, Quaternion.identity, _cubeParent);
        newCube.Init(scale, color, splitChance);
        
        return newCube;
    }

    public Cube Create(Vector3 scale, float splitChance)
    {
        Vector3 spawnPosition = GetSpawnPosition(_cubePrefab.transform.localScale);
        return Create(spawnPosition, scale, GetColor(), splitChance);
    }
    
    public Cube Create(Cube oldCube)
    {
        Vector3 position = oldCube.transform.position;
        Vector3 scale = oldCube.transform.localScale / 2;
        Color color = GetColor();
        float splitChance = oldCube.SplitChance / 2;

        Cube newCube = Create(position, scale, color, splitChance);
        newCube.Rigidbody.AddExplosionForce(_explosionForce, oldCube.transform.position, _explosionRadius);
        return newCube;
    }

    private Vector3 GetSpawnPosition(Vector3 scale)
    {
        Vector3 newPosition;
        bool isPositionTaken;
        
        do
        {
            newPosition = GetRandomPosition();
            isPositionTaken = Physics.BoxCast(newPosition, scale / 2, Vector3.zero);
        } while (isPositionTaken);

        return newPosition;
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x + 1);
        float z = Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z + 1);
        float y = _spawnZone.bounds.center.y;
        
        return new Vector3(x, y, z);
    }
    
    private Color GetColor()
        => Random.ColorHSV();
}
