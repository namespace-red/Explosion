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

    private void Spawn(Cube explodedCube = null)
    {
        int spawnCubeNumber = GetSpawnNumber();
        
        for (int i = 0; i < spawnCubeNumber; i++)
        {
            Cube newCube = (explodedCube == null) ? _cubeFactory.Create() 
                : _cubeFactory.Create(explodedCube);
            
            newCube.Spliting += OnCubeSpliting;
        }
    }

    private void OnCubeSpliting(Cube splitedCube)
    {
        splitedCube.Spliting -= OnCubeSpliting;

        Spawn(splitedCube);
    }

    private int GetSpawnNumber()
        => Random.Range(_minSpawnCubs, _maxSpawnCubs + 1);
}
