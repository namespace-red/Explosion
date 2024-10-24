using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private int _minSpawnCubs = 2;
    [SerializeField] private int _maxSpawnCubs = 6;

    private void Awake()
    {
        Spawn();
    }

    private void OnCubeExploded(Cube explodedCube)
    {
        explodedCube.Exploded -= OnCubeExploded;

        Spawn(explodedCube);
    }

    private void Spawn(Cube explodedCube = null)
    {
        int spawnCubeNumber = GetSpawnNumber();
        
        for (int i = 0; i < spawnCubeNumber; i++)
        {
            Cube newCube = (explodedCube == null) ? _cubeFactory.Create() 
                : _cubeFactory.Create(explodedCube);
            
            newCube.Exploded += OnCubeExploded;
        }
    }
    
    private int GetSpawnNumber()
        => Random.Range(_minSpawnCubs, _maxSpawnCubs + 1);
}
