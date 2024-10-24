using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private const int MaxSplitChance = 100;
    private const int MinSplitChance = 0;

    private Material _material;
    public event Action<Cube> Exploded;
    
    public float SplitChance { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        TryToSplit();
    }

    public void Init(Vector3 scale, Color color, float splitChance)
    {
        transform.localScale = scale;
        _material.color = color;
        SplitChance = splitChance;
    }

    private void TryToSplit()
    {
        float chance = Random.Range(MinSplitChance, MaxSplitChance + 1);

        if (chance <= SplitChance)
        {
            Exploded?.Invoke(this);
        }
        
        Destroy(gameObject);
    }
}
