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
        _spawner.Spawn += OnSpawn;
    }

    private void OnDisable()
    {
        _spawner.Spawn -= OnSpawn;
    }

    private void OnSpawn(Vector3 position)
    {
        Explode(position);
    }

    private void Explode(Vector3 position)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(position))
            explodableObject.AddExplosionForce(_explosionForce, position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRadius);

        List<Rigidbody> cubesToMove = new List<Rigidbody>();
        
        foreach (Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
                cubesToMove.Add(hit.attachedRigidbody);            
        }

        return cubesToMove;
    }
}
