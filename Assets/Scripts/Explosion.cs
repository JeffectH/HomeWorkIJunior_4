using System.Collections.Generic;
using UnityEngine;

public class Explosion
{
    private float _expousionRadius = 20f;
    private float _expousionForce = 300f;

    public void Expode(Vector3 pointExplode)
    {
        foreach (Rigidbody explodableObjects in GetExplousionObjects(pointExplode))
        {
            explodableObjects.AddExplosionForce(_expousionForce, pointExplode, _expousionRadius);
        }
    }

    private List<Rigidbody> GetExplousionObjects(Vector3 pointExplode)
    {
        var hits = Physics.OverlapSphere(pointExplode, _expousionRadius);

        var cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody is not null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
    
    public void ExplodeForAllExcept(Vector3 pointExplode, Cube except, float radius, float force)
    {
        var hits = Physics.OverlapSphere(pointExplode, radius);
        foreach (var hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;
            if (rb != null && rb.gameObject != except.gameObject)
            {
                rb.AddExplosionForce(force, pointExplode, radius);
            }
        }
    }

}
