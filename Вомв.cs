using System.Collections.Generic;
using UnityEngine;

public class Вомв : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private CubeLogic _cubeLogic;
    [SerializeField] private ParticleSystem _effect;

    private void OnEnable()
    {
        _cubeLogic.BlownUp += OnBlownUp;
    }

    private void OnDisable()
    {
        _cubeLogic.BlownUp -= OnBlownUp;
    }

    private void OnBlownUp (CubeLogic cubeLogic)
    {
        float sizeBonus = cubeLogic.ChanceToReborn;

        Instantiate(_effect, transform.position, transform.rotation);

        Explode(sizeBonus);
    }

    private void Explode(float sizeBonus)
    {
        foreach (Rigidbody cube in GetExplodableObjects(_explosionRadius/sizeBonus))
        {
            float distance = Vector3.Distance(transform.position, cube.position);
            float force = _explosionForce / (sizeBonus + distance);

            cube.AddExplosionForce(force, transform.position, _explosionRadius/sizeBonus);
        }
    }

    private List<Rigidbody> GetExplodableObjects(float radius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
