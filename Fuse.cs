using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.Spawned += OnSpawned;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnSpawned;
    }

    private void OnSpawned(Vector3 position, List<Rigidbody> cubesToMove)
    {
        foreach (Rigidbody cube in cubesToMove)
        { 
            cube.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }    
}
