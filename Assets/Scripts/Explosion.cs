using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion
{
    private const float BaseRadius = 20f;
    private const float BaseForce = 300f;

    public void Expode(Vector3 pointExplode, Vector3 cubeScale)
    {
        float radius = GetRadius(cubeScale);
        var objects = GetExplosionObjects(pointExplode, radius);

        foreach (Rigidbody explodableObjects in objects)
        {
            Vector3 direction = (explodableObjects.transform.position - pointExplode).normalized;
            float distance = Vector3.Distance(explodableObjects.transform.position, pointExplode);
            float force = Mathf.Max(0f, GetForce(distance, radius, cubeScale));
            explodableObjects.AddForce(direction * force, ForceMode.Force);
        }
    }

    private List<Rigidbody> GetExplosionObjects(Vector3 pointExplode, float radius)
    {
        var hits = Physics.OverlapSphere(pointExplode, radius);
        return hits.Where(hit => hit.attachedRigidbody != null).Select(hit => hit.attachedRigidbody).ToList();
    }

    private float GetForce(float distance, float radius, Vector3 cubeScale)
    {
        float normalizedDistance = distance / GetRadius(cubeScale);
        return BaseForce / cubeScale.x * (1f - normalizedDistance);
    }

    private float GetRadius(Vector3 cubeScale)
        => BaseRadius / cubeScale.x;
}
